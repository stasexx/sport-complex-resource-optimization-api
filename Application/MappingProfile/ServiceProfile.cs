using AutoMapper;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Application.MappingProfile;

public class ServiceProfile : Profile
{
    public  ServiceProfile()
    {
        CreateMap<Service, ServiceCreateDto>();
        CreateMap<ServiceCreateDto, Service>();
    }
}