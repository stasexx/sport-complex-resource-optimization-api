using System.Data;
using System.Security.Claims;
using AutoMapper;
using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.IServices.Identity;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Models.Dtos;
using SportComplexResourceOptimizationApi.Application.Models.Identity;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;
using SportComplexResourceOptimizationApi.Application.Paging;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Infrastructure.Services;

public class UsersService : IUserService 
{
    private readonly IUsersRepository _usersRepository;

    private readonly IRolesRepository _rolesRepository;

    private readonly ITokensService _tokensService;

    private IRefreshTokensRepository _refreshTokensRepository;

    private readonly IMapper _mapper;

    private readonly IPasswordHasher _passwordHasher;

    public UsersService(IUsersRepository usersRepository, IRolesRepository rolesRepository, ITokensService tokensService, 
    IRefreshTokensRepository refreshTokensRepository, IPasswordHasher passwordHasher, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _rolesRepository = rolesRepository;
        _tokensService = tokensService;
        _refreshTokensRepository = refreshTokensRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public async Task<PagedList<UserDto>> GetUsersPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var entities = await _usersRepository.GetPageAsync(pageNumber, pageSize, cancellationToken);
        var dtos = _mapper.Map<List<UserDto>>(entities);
        var count = await _usersRepository.GetTotalCountAsync(cancellationToken);
        return new PagedList<UserDto>(dtos, pageNumber, pageSize, count);
    }

    public async Task<UserDto> GetUserAsync(string id, CancellationToken cancellationToken)
    {
        if (!ObjectId.TryParse(id, out var objectId))
        {
            throw new InvalidDataException("Provided id is invalid.");
        }

        var entity = await _usersRepository.GetOneAsync(objectId, cancellationToken);
        if (entity == null)
        {
            throw new Exception(id);
        }

        return _mapper.Map<UserDto>(entity);
    }

    public async Task<UserDto> AddUserAsync(UserCreateDto dto, CancellationToken cancellationToken)
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
            Roles = new List<Role>{role},
            Phone = dto.Phone,
            PasswordHash = this._passwordHasher.Hash(dto.Password),
            CreatedDateUtc = DateTime.UtcNow,
            CreatedById = ObjectId.Empty
        };

        await _usersRepository.AddAsync(user, cancellationToken);
        var refreshToken = await AddRefreshToken(user.Id, cancellationToken);

        return _mapper.Map<UserDto>(user);
    }

    public async Task<TokensModel> LoginAsync(LoginUserDto login, CancellationToken cancellationToken)
    {

        var user = await _usersRepository.GetOneAsync(u => u.Email == login.Email, cancellationToken);

        if (user == null)
        {
            throw new Exception("User");
        }

        if (!this._passwordHasher.Check(login.Password, user.PasswordHash))
        {
            throw new InvalidDataException("Invalid password!");
        }

        var refreshToken = await AddRefreshToken(user.Id, cancellationToken);

        var tokens = this.GetUserTokens(user, refreshToken);

        return tokens;
    }

    public async Task<UpdateUserModel> UpdateAsync(UserUpdateDto userDto, CancellationToken cancellationToken)
    {

        var user = await this._usersRepository.GetOneAsync(x => x.Id == ObjectId.Parse(userDto.Id), cancellationToken);
        if (user == null)
        {
            throw new Exception("User");
        }

        // TODO: Cleanup
        var userValidationDto = new UserDto 
        { 
            Email = userDto.Email, 
            Phone = userDto.Phone
        };

        this._mapper.Map(userDto, user);
        if (!string.IsNullOrEmpty(userDto.Password))
        {
            user.PasswordHash = this._passwordHasher.Hash(userDto.Password);
        }

        //await CheckAndUpgradeToUserAsync(user, cancellationToken);

        var updatedUser = await this._usersRepository.UpdateUserAsync(user, cancellationToken);

        var refreshToken = await AddRefreshToken(user.Id, cancellationToken);

        var tokens = this.GetUserTokens(user, refreshToken);

        var updatedUserDto = this._mapper.Map<UserDto>(updatedUser);

        return new UpdateUserModel() 
        { 
            Tokens = tokens, 
            User = updatedUserDto
        };
    }

    public async Task<UserDto> AddToRoleAsync(string userId, string roleName, CancellationToken cancellationToken)
    {
        var role = await _rolesRepository.GetOneAsync(r => r.Name == roleName, cancellationToken);

        if (role==null)
        {
            throw new ArgumentNullException($"{roleName} is not found");
        }

        var userObjectId = ObjectId.Parse(userId);
        var user = await _usersRepository.GetOneAsync(userObjectId, cancellationToken);

        if (user == null)
        {
            throw new ArgumentNullException("User");
        }
        
        user.Roles.Add(role);
        var updateUser = await this._usersRepository.UpdateUserAsync(user, cancellationToken);
        var userDto = this._mapper.Map<UserDto>(updateUser);

        return userDto;
    }
    
    public async Task<UserDto> RemoveFromRoleAsync(string userId, string roleName, CancellationToken cancellationToken)
    {
        var role = await _rolesRepository.GetOneAsync(r => r.Name == roleName, cancellationToken);

        if (role==null)
        {
            throw new ArgumentNullException($"{roleName} is not found");
        }

        var userObjectId = ObjectId.Parse(userId);
        var user = await _usersRepository.GetOneAsync(userObjectId, cancellationToken);

        if (user == null)
        {
            throw new ArgumentNullException("User");
        }

        var deletedRole = user.Roles.Find(r => r.Name == role.Name);
        user.Roles.Remove(deletedRole);
        
        var updateUser = await this._usersRepository.UpdateUserAsync(user, cancellationToken);
        var userDto = this._mapper.Map<UserDto>(updateUser);

        return userDto;
    }

    private TokensModel GetUserTokens(User user, RefreshToken refreshToken)
    {
        var claims = this.GetClaims(user);
        var accessToken = this._tokensService.GenerateAccessToken(claims);


        return new TokensModel
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
        };
    }

    private IEnumerable<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>()
        {
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Email, user.Email ?? string.Empty),
            new (ClaimTypes.MobilePhone, user.Phone ?? string.Empty),
        };

        return claims;
    }
    
    private async Task<RefreshToken> AddRefreshToken(ObjectId userId, CancellationToken cancellationToken)
    {
        var refreshToken = new RefreshToken
        {
            Token = _tokensService.GenerateRefreshToken(),
            ExpiryDateUTC = DateTime.UtcNow.AddDays(10),
            CreatedDateUtc = DateTime.UtcNow
        };

        await this._refreshTokensRepository.AddAsync(refreshToken, cancellationToken);

        return refreshToken;
    }
}