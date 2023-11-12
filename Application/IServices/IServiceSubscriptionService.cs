using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Application.IServices;

public interface IServiceSubscriptionService
{
    Task<ServiceSubscriptionCreateDto> CreateAbonnement(ServiceSubscriptionCreateDto dto, string userId,
        string abonnementId,
        CancellationToken cancellationToken);

    Task UpdateUsages(string id, CancellationToken cancellationToken);
}