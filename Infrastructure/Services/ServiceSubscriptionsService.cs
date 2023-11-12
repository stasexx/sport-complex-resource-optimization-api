using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Infrastructure.Services;

public class ServiceSubscriptionsService : IServiceSubscriptionService
{
    private readonly IServiceSubscriptionsRepository _serviceSubscriptionsRepository;

    private readonly IAbonnementsRepository _abonnementsRepository;

    public ServiceSubscriptionsService(IServiceSubscriptionsRepository serviceSubscriptionsRepository, IAbonnementsRepository abonnementsRepository)
    {
        _serviceSubscriptionsRepository = serviceSubscriptionsRepository;
        _abonnementsRepository = abonnementsRepository;
    }
    
    public async Task<ServiceSubscriptionCreateDto> CreateAbonnement(ServiceSubscriptionCreateDto dto, string userId, string abonnementId,
        CancellationToken cancellationToken)
    {
        
        var serviceSubscription = new ServiceSubscription()
        {
            RemainingUsages = _abonnementsRepository.GetDuration(abonnementId).Result,
            AbonnementId = ObjectId.Parse(abonnementId),
            CreatedById = ObjectId.Parse(userId),
            CreatedDateUtc = DateTime.UtcNow
        };

        await _serviceSubscriptionsRepository.AddAsync(serviceSubscription, cancellationToken);

        return dto;
    }

    public async Task UpdateUsages(string id, CancellationToken cancellationToken)
    {
        await _serviceSubscriptionsRepository.UpdateUsages(id);
    }
}