using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Domain.Common;

namespace SportComplexResourceOptimizationApi.Domain.Entities;

public class UsagesHistory : EntityBase
{
    public int TotalUsages { get; set; }
    
    public ObjectId EquipmentId { get; set; }
}