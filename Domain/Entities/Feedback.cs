using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Domain.Common;

namespace SportComplexResourceOptimizationApi.Domain.Entities;

public class Feedback : EntityBase
{
    public string Description { get; set; }
    
    public double Rating { get; set; }
    
    public ObjectId SportComplexId { get; set; }
}