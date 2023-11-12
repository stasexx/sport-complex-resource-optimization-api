using Application.Models.Dtos;
using AutoMapper;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Application.MappingProfile;

public class ServiceProfile : Profile
{
    public  ServiceProfile()
    {
        CreateMap<Service, ServiceCreateDto>();
        CreateMap<ServiceCreateDto, Service>();
        CreateMap<ServiceDto, Service>();
        CreateMap<Service, ServiceDto>();
        CreateMap<ServiceUpdateDto, Service>();
        CreateMap<Service, ServiceUpdateDto>();
    }
}