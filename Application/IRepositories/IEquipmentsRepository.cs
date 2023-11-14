using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Application.IRepositories;

public interface IEquipmentsRepository : IBaseRepository<Equipment>
{
    Task<Equipment> RevealEquipmentAsync(string equipmentId, CancellationToken cancellationToken);

    Task<Equipment> UpdateEquipmentAsync(Equipment equipment, CancellationToken cancellationToken);

    Task<string> GetEquipmentNameById(ObjectId equipmentId);
}