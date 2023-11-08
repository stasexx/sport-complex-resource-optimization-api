using AutoMapper;
using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Infrastructure.Services;

public class ServicesService : IServiceService
{
    private readonly IServicesRepository _servicesRepository;

    private readonly IMapper _mapper;

    public ServicesService(IServicesRepository serviceRepository, IMapper mapper)
    {
        _servicesRepository = serviceRepository;
        _mapper = mapper;
    }

    public async Task<ServiceCreateDto> CreateService(ServiceCreateDto dto, string complexId,
        CancellationToken cancellationToken)
    {
        var service = new Service()
        {
            Name = dto.Name,
            CreatedById = ObjectId.Parse(complexId),
            CreatedDateUtc = DateTime.UtcNow
        };
        await _servicesRepository.AddAsync(service, cancellationToken);
        return dto;
    }
}