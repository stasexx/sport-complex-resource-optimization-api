using SportComplexResourceOptimizationApi.Application.Models.CreateDto;

namespace SportComplexResourceOptimizationApi.Application.IServices;

public interface IServiceService
{
    Task<ServiceCreateDto> CreateService(ServiceCreateDto dto, string complexId, CancellationToken cancellationToken);
}