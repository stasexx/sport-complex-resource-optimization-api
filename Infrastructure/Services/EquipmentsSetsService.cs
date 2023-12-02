using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Infrastructure.Services;

public class EquipmentsSetsService : IEquipmentsSetsService
{
    private readonly IEquipmentsSetsRepository _equipmentsSetsRepository;

    public EquipmentsSetsService(IEquipmentsSetsRepository equipmentsSetsRepository)
    {
        _equipmentsSetsRepository = equipmentsSetsRepository;
    }

    public async Task<EquipmentsSetCreateDto> CreateNewSet(EquipmentsSetCreateDto dto,
        CancellationToken cancellationToken)
    {
        List<ObjectId> objectIdList = dto.Equipments.Select(ObjectId.Parse).ToList();

        var newSet = new EquipmentsSet()
        {
            EquipmentSetName = dto.Name,
            EquipmentsIds = objectIdList,
            CreatedById = ObjectId.Parse(dto.UserId),
        };

        await _equipmentsSetsRepository.AddAsync(newSet, cancellationToken);

        return dto;
    }
}