using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Domain.Common;

namespace SportComplexResourceOptimizationApi.Domain.Entities;

public class Equipment : EntityBase 
{
    public string Name { get; set; }
    
    public ObjectId ServiceId { get; set; }
}