using Application.Models.Dtos;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Application.IServices;

public interface IEquipmentService
{
    Task<EquipmentDto> CreateEquipment(EquipmentDto dto, string serviceId,
        CancellationToken cancellationToken);
}