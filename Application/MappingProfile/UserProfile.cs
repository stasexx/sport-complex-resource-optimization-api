using AutoMapper;
using SportComplexResourceOptimizationApi.Application.Models.Dtos;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Application.MappingProfile;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        CreateMap<UserUpdateDto, User>();
        CreateMap<User, UserUpdateDto>();
    }
}