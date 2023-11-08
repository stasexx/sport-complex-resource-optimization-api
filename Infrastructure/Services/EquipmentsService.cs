using Application.Models.Dtos;
using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Infrastructure.Services;

public class EquipmentsService : IEquipmentService
{
    private readonly IEquipmentsRepository _equipmentRepository;

    public EquipmentsService(IEquipmentsRepository equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;
    }

    public async Task<EquipmentDto> CreateEquipment(EquipmentDto dto, string serviceId,
        CancellationToken cancellationToken)
    {
        var equipment = new Equipment()
        {
            Name = dto.Name,
            CreatedById = ObjectId.Parse(serviceId),
            CreatedDateUtc = DateTime.UtcNow
        };

        await _equipmentRepository.AddAsync(equipment, cancellationToken);

        return dto;
    }
}