using SportComplexResourceOptimizationApi.Application.Models.CreateDto;

namespace SportComplexResourceOptimizationApi.Application.IServices;

public interface IAbonnementService
{
    Task<AbonnementCreateDto> CreateAbonnement(AbonnementCreateDto dto, string serviceId,
        CancellationToken cancellationToken);
}