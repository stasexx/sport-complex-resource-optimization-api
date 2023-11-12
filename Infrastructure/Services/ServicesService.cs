using Application.Models.Dtos;
using AutoMapper;
using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;
using SportComplexResourceOptimizationApi.Application.Paging;
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

    public async Task<ServiceCreateDto> CreateService(ServiceCreateDto dto, string userId,
        CancellationToken cancellationToken)
    {
        var service = new Service()
        {
            Name = dto.Name,
            SportComplexId = ObjectId.Parse(dto.SportComplexId),
            CreatedById = ObjectId.Parse(userId),
            CreatedDateUtc = DateTime.UtcNow
        };
        await _servicesRepository.AddAsync(service, cancellationToken);
        return dto;
    }

    public async Task<ServiceDto> UpdateService(ServiceUpdateDto service, CancellationToken cancellationToken)
    {
        var serv = await _servicesRepository.GetOneAsync(c => c.Id == ObjectId.Parse(service.Id), cancellationToken);

        this._mapper.Map(service, serv);
        
        var result = await _servicesRepository.UpdateServiceAsync(serv, cancellationToken);
        
        return _mapper.Map<ServiceDto>(result);
    }
    
    public async Task<ServiceDto> DeleteService(string serviceId, CancellationToken cancellationToken)
    {
        var service = await _servicesRepository.GetOneAsync(c => c.Id == ObjectId.Parse(serviceId), cancellationToken);

        if (service == null)
        {
            throw new Exception("Service was not found!");
        }

        await _servicesRepository.DeleteFromDbAsync(service, cancellationToken);

        return _mapper.Map<ServiceDto>(service);
    }

    public async Task<ServiceDto> HideService(string serviceId, CancellationToken cancellationToken)
    {
        var service = await _servicesRepository.GetOneAsync(c => c.Id == ObjectId.Parse(serviceId), cancellationToken);

        if (service == null)
        {
            throw new Exception("Service was not found!");
        }

        await _servicesRepository.DeleteAsync(service, cancellationToken);

        return _mapper.Map<ServiceDto>(service);
    }
    
    public async Task<ServiceDto> RevealService(string serviceId, CancellationToken cancellationToken)
    {
        var service = await _servicesRepository.GetOneAsync(c => c.Id == ObjectId.Parse(serviceId), cancellationToken);

        if (service == null)
        {
            throw new Exception("Service was not found!");
        }

        await _servicesRepository.RevealServiceAsync(serviceId, cancellationToken);

        return _mapper.Map<ServiceDto>(service);
    }
    
    public async Task<PagedList<ServiceDto>> GetServicePages(int pageNumber, int pageSize, string sportComplexId,
        CancellationToken cancellationToken)
    {
        var entities = await _servicesRepository.GetPageAsync(pageNumber, pageSize,
            x=> x.SportComplexId==ObjectId.Parse(sportComplexId), cancellationToken);
        var dtos = _mapper.Map<List<ServiceDto>>(entities);
        var count = await _servicesRepository.GetTotalCountAsync(cancellationToken);
        return new PagedList<ServiceDto>(dtos, pageNumber, pageSize, count);
    }

    public async Task<PagedList<ServiceDto>> GetVisibleServicePages(int pageNumber, int pageSize, string sportComplexId,
        CancellationToken cancellationToken)
    {
        var entities = await _servicesRepository.GetPageAsync(pageNumber, pageSize, x=> x.IsDeleted==false
            && x.SportComplexId == ObjectId.Parse(sportComplexId), cancellationToken);
        var dtos = _mapper.Map<List<ServiceDto>>(entities);
        var count = await _servicesRepository.GetTotalCountAsync(cancellationToken);
        return new PagedList<ServiceDto>(dtos, pageNumber, pageSize, count);
    }
}