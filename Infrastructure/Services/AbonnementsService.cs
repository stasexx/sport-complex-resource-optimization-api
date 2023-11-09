using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Infrastructure.Services;

public class AbonnementsService : IAbonnementService
{
    private readonly IAbonnementsRepository _abonnementsRepository;

    public AbonnementsService(IAbonnementsRepository abonnementsRepository)
    {
        _abonnementsRepository = abonnementsRepository;
    }

    public async Task<AbonnementCreateDto> CreateAbonnement(AbonnementCreateDto dto, string serviceId,
        CancellationToken cancellationToken)
    {
        var abonnement = new Abonnement()
        {
            Name = dto.Name,
            Duration = dto.Duration,
            Price = dto.Price,
            CreatedById = ObjectId.Parse(serviceId),
            CreatedDateUtc = DateTime.UtcNow
        };

        await _abonnementsRepository.AddAsync(abonnement, cancellationToken);

        return dto;
    }
}