using System.Data;
using AutoMapper;
using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Application.IRepository;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.IServices.Identity;
using SportComplexResourceOptimizationApi.Application.Models;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Models.Dtos;
using SportComplexResourceOptimizationApi.Domain.Entities;
using SportComplexResourceOptimizationApi.Entities;

namespace SportComplexResourceOptimizationApi.Infrastructure.Services;

public class UsersService : IUserService 
{
    private readonly IUsersRepository _usersRepository;

    private readonly IRolesRepository _rolesRepository;

    private readonly ITokensService _tokensService;

    private IRefreshTokensRepository _refreshTokensRepository;

    private readonly IMapper _mapper;


    public UsersService(IUsersRepository usersRepository, IRolesRepository rolesRepository, ITokensService tokensService, 
    IRefreshTokensRepository refreshTokensRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _rolesRepository = rolesRepository;
        _tokensService = tokensService;
        _refreshTokensRepository = refreshTokensRepository;
        _mapper = mapper;
    }

    public async Task AddUserAsync(UserCreateDto dto, CancellationToken cancellationToken)
    {
        var userDto = new UserDto 
        { 
            Email = dto.Email, 
            Phone = dto.Phone
        };

        var role = await _rolesRepository.GetOneAsync(r => r.Name == "User", cancellationToken);
        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
            PasswordHash = dto.Password,
            CreatedDateUtc = DateTime.UtcNow,
            CreatedById = ObjectId.Empty
        };

        await _usersRepository.AddAsync(user, cancellationToken);
        var refreshToken = await AddRefreshToken(user.Id, cancellationToken);
    }

    private async Task<RefreshToken> AddRefreshToken(ObjectId userId, CancellationToken cancellationToken)
    {
        var refreshToken = new RefreshToken
        {
            Token = _tokensService.GenerateRefreshToken(),
            ExpiryDateUTC = DateTime.UtcNow.AddDays(30),
            CreatedById = userId,
            CreatedDateUtc = DateTime.UtcNow
        };

        await this._refreshTokensRepository.AddAsync(refreshToken, cancellationToken);

        return refreshToken;
    }
}