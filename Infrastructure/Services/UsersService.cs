using AutoMapper;
using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Application.IRepository;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models;
using SportComplexResourceOptimizationApi.Entities;

namespace SportComplexResourceOptimizationApi.Infrastructure.Services;

public class UsersService : IUserService 
{
    private readonly IUsersRepository _userRepository;

    private readonly IMapper _mapper;

    public UsersService(IUsersRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task AddUserAsync(UserDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<User>(dto);
        await _userRepository.AddAsync(entity, cancellationToken);
    }
}