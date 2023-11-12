using Application.Models.Dtos;
using AutoMapper;
using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Paging;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Infrastructure.Services;

public class AbonnementsService : IAbonnementService
{
    private readonly IAbonnementsRepository _abonnementsRepository;

    private readonly IMapper _mapper;

    public AbonnementsService(IAbonnementsRepository abonnementsRepository, IMapper mapper)
    {
        _abonnementsRepository = abonnementsRepository;
        _mapper = mapper;
    }

    public async Task<AbonnementCreateDto> CreateAbonnement(AbonnementCreateDto dto, string serviceId, string userId,
        CancellationToken cancellationToken)
    {
        var abonnement = new Abonnement()
        {
            Name = dto.Name,
            Duration = dto.Duration,
            Price = dto.Price,
            ServiceId = ObjectId.Parse(serviceId),
            CreatedById = ObjectId.Parse(userId),
            CreatedDateUtc = DateTime.UtcNow
        };

        await _abonnementsRepository.AddAsync(abonnement, cancellationToken);

        return dto;
    }
    
    public async Task<AbonnementDto> UpdateAbonnement(AbonnementDto abonnementDto, CancellationToken cancellationToken)
    {
        var abonnement = await _abonnementsRepository.GetOneAsync(c => c.Id == ObjectId.Parse(abonnementDto.Id), cancellationToken);

        this._mapper.Map(abonnementDto, abonnement);
        
        var result = await _abonnementsRepository.UpdateAbonnementAsync(abonnement, cancellationToken);
        
        return _mapper.Map<AbonnementDto>(result);
    }
    
    public async Task<AbonnementDto> DeleteAbonnement(string abonnementId, CancellationToken cancellationToken)
    {
        var abonnement = await _abonnementsRepository.GetOneAsync(c => c.Id == ObjectId.Parse(abonnementId), cancellationToken);

        if (abonnement == null)
        {
            throw new Exception("Aonnement was not found!");
        }

        await _abonnementsRepository.DeleteFromDbAsync(abonnement, cancellationToken);

        return _mapper.Map<AbonnementDto>(abonnement);
    }

    public async Task<ServiceDto> HideAbonnement(string abonnementId, CancellationToken cancellationToken)
    {
        var abonnement = await _abonnementsRepository.GetOneAsync(c => c.Id == ObjectId.Parse(abonnementId), cancellationToken);

        if (abonnement == null)
        {
            throw new Exception("Abonnement was not found!");
        }

        await _abonnementsRepository.DeleteAsync(abonnement, cancellationToken);

        return _mapper.Map<ServiceDto>(abonnement);
    }
    
    public async Task<AbonnementDto> RevealAbonnement(string abonnementId, CancellationToken cancellationToken)
    {
        var abonnement = await _abonnementsRepository.GetOneAsync(c => c.Id == ObjectId.Parse(abonnementId), cancellationToken);

        if (abonnement == null)
        {
            throw new Exception("Abonnement was not found!");
        }

        await _abonnementsRepository.RevealAbonnementAsync(abonnementId, cancellationToken);

        return _mapper.Map<AbonnementDto>(abonnement);
    }
    
    public async Task<PagedList<AbonnementDto>> GetAbonnementPages(int pageNumber, int pageSize, string serviceId,
        CancellationToken cancellationToken)
    {
        var entities = await _abonnementsRepository.GetPageAsync(pageNumber, pageSize,
            x=> x.ServiceId==ObjectId.Parse(serviceId), cancellationToken);
        var dtos = _mapper.Map<List<AbonnementDto>>(entities);
        var count = await _abonnementsRepository.GetTotalCountAsync(cancellationToken);
        return new PagedList<AbonnementDto>(dtos, pageNumber, pageSize, count);
    }

    public async Task<PagedList<AbonnementDto>> GetVisibleAbonnementPages(int pageNumber, int pageSize, string serviceId,
        CancellationToken cancellationToken)
    {
        var entities = await _abonnementsRepository.GetPageAsync(pageNumber, pageSize, x=> x.IsDeleted==false
            && x.ServiceId == ObjectId.Parse(serviceId), cancellationToken);
        var dtos = _mapper.Map<List<AbonnementDto>>(entities);
        var count = await _abonnementsRepository.GetTotalCountAsync(cancellationToken);
        return new PagedList<AbonnementDto>(dtos, pageNumber, pageSize, count);
    }
}