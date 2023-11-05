using AutoMapper;
using SportComplexResourceOptimizationApi.Application.Models;
using SportComplexResourceOptimizationApi.Domain.Entities;
using SportComplexResourceOptimizationApi.Entities;

namespace SportComplexResourceOptimizationApi.Application.MappingProfile;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<UserDto, User>();
    }
}