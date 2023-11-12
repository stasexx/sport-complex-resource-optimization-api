using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Domain.Common;

namespace SportComplexResourceOptimizationApi.Domain.Entities;

public class Sensor : EntityBase 
{
    public bool Status { get; set; }
    
    public ObjectId EquipmentId { get; set; }
}