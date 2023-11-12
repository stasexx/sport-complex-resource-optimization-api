using Application.Models.Dtos;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;
using SportComplexResourceOptimizationApi.Application.Paging;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Application.IServices;

public interface IEquipmentService
{
    Task<EquipmentDto> CreateEquipment(EquipmentDto dto, string serviceId, string userId,
        CancellationToken cancellationToken);

    Task<EquipmentDto> UpdateEquipment(EquipmentUpdateDto equipmentDto, CancellationToken cancellationToken);

    Task<EquipmentDto> DeleteEquipment(string equipmentId, CancellationToken cancellationToken);

    Task<EquipmentDto> HideEquipment(string equipmentId, CancellationToken cancellationToken);

    Task<EquipmentDto> RevealEquipment(string equipmentId, CancellationToken cancellationToken);

    Task<PagedList<EquipmentDto>> GetEquipmentPages(int pageNumber, int pageSize, string serviceId,
        CancellationToken cancellationToken);

    Task<PagedList<EquipmentDto>> GetVisibleEquipmentPages(int pageNumber, int pageSize, string serviceId,
        CancellationToken cancellationToken);

    Task<bool> GetStatus(string equipmentId, CancellationToken cancellationToken);
}