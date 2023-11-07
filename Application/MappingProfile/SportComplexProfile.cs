using Application.Models.Dtos;
using AutoMapper;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Application.MappingProfile;

public class SportComplexProfile : Profile
{
    public SportComplexProfile()
    {
        CreateMap<SportComplex, SportComplexDto>().ReverseMap();
        CreateMap<SportComplexDto, SportComplex>();
        CreateMap<SportComplexCreateDto, SportComplex>();
        CreateMap<SportComplex, SportComplexCreateDto>();
    }
}