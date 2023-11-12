using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;

namespace SportComplexResourceOptimizationApi.Application.IServices;

public interface ISensorService
{
    Task<SensorCreateDto> CreateSensor(SensorCreateDto dto, CancellationToken cancellationToken);

    Task<SensorUpdateDto> UpdateStatus(SensorUpdateDto dto, CancellationToken cancellationToken);
}