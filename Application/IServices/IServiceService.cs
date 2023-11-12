using Application.Models.Dtos;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;
using SportComplexResourceOptimizationApi.Application.Paging;

namespace SportComplexResourceOptimizationApi.Application.IServices;

public interface IServiceService
{
    Task<ServiceCreateDto> CreateService(ServiceCreateDto dto, string complexId, CancellationToken cancellationToken);

    Task<ServiceDto> UpdateService(ServiceUpdateDto service, CancellationToken cancellationToken);

    Task<ServiceDto> HideService(string sportComplexId, CancellationToken cancellationToken);

    Task<ServiceDto> DeleteService(string sportComplexId, CancellationToken cancellationToken);

    Task<ServiceDto> RevealService(string serviceId, CancellationToken cancellationToken);

    Task<PagedList<ServiceDto>> GetVisibleServicePages(int pageNumber, int pageSize, string sportComplexId,
        CancellationToken cancellationToken);
    
    Task<PagedList<ServiceDto>> GetServicePages(int pageNumber, int pageSize, string sportComplexId,
        CancellationToken cancellationToken);
}