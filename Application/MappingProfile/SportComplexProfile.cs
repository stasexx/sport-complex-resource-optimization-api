using Application.Models.Dtos;
using AutoMapper;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Application.MappingProfile;

public class SportComplexProfile : Profile
{
    public SportComplexProfile()
    {
        CreateMap<SportComplex, SportComplexDto>();
        CreateMap<SportComplexDto, SportComplex>();
        CreateMap<SportComplexUpdateDto, SportComplex>();
        CreateMap<SportComplexCreateDto, SportComplex>();
        CreateMap<SportComplex, SportComplexCreateDto>();
    }
}