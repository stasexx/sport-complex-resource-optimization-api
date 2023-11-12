using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Domain.Common;

namespace SportComplexResourceOptimizationApi.Domain.Entities;

public class Service : EntityBase
{
    public string Name { get; set; }
    
    public ObjectId SportComplexId { get; set; }
}