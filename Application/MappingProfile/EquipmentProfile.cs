using Application.Models.Dtos;
using AutoMapper;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Application.MappingProfile;

public class EquipmentProfile : Profile
{
    public EquipmentProfile()
    {
        CreateMap<Equipment, EquipmentDto>();
        CreateMap<EquipmentDto, Equipment>();
        CreateMap<EquipmentUpdateDto, Equipment>();
        CreateMap<Equipment, EquipmentUpdateDto>();
    }
}