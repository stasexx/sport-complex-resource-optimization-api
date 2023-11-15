using SportComplexResourceOptimizationApi.Application.Models.CreateDto;

namespace SportComplexResourceOptimizationApi.Application.IServices;

public interface IEquipmentsSetsService
{
    Task<EquipmentsSetCreateDto> CreateNewSet(EquipmentsSetCreateDto dto,
        CancellationToken cancellationToken);
}