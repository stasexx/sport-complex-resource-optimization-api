using Application.Models.Dtos;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Paging;

namespace SportComplexResourceOptimizationApi.Application.IServices;

public interface IAbonnementService
{
    Task<AbonnementCreateDto> CreateAbonnement(AbonnementCreateDto dto, string serviceId, string userId,
        CancellationToken cancellationToken);

    Task<AbonnementDto> UpdateAbonnement(AbonnementDto abonnementDto, CancellationToken cancellationToken);

    Task<AbonnementDto> DeleteAbonnement(string abonnementId, CancellationToken cancellationToken);

    Task<ServiceDto> HideAbonnement(string abonnementId, CancellationToken cancellationToken);

    Task<AbonnementDto> RevealAbonnement(string abonnementId, CancellationToken cancellationToken);

    Task<PagedList<AbonnementDto>> GetAbonnementPages(int pageNumber, int pageSize, string serviceId,
        CancellationToken cancellationToken);

    Task<PagedList<AbonnementDto>> GetVisibleAbonnementPages(int pageNumber, int pageSize, string serviceId,
        CancellationToken cancellationToken);
}