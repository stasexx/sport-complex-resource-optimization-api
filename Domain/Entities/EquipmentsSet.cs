using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Domain.Common;

namespace SportComplexResourceOptimizationApi.Domain.Entities;

public class EquipmentsSet : EntityBase
{
    public string EquipmentSetName { get; set; }
    
    public List<ObjectId> EquipmentsIds { get; set; }
}