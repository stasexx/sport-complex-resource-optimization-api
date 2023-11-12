using Application.Models.Dtos;
using AutoMapper;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Application.MappingProfile;

public class AbonnementProfile : Profile
{
    public AbonnementProfile()
    {
        CreateMap<Abonnement, AbonnementDto>();
        CreateMap<AbonnementDto, Abonnement>();
        CreateMap<AbonnementCreateDto, Abonnement>();
        CreateMap<Abonnement, AbonnementCreateDto>();
    }
}