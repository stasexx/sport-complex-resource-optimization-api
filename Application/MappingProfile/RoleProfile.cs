using AutoMapper;
using SportComplexResourceOptimizationApi.Application.Models;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Application.MappingProfile;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<RoleDto, Role>();
    }
}