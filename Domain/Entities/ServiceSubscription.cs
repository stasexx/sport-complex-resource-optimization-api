using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Domain.Common;

namespace SportComplexResourceOptimizationApi.Domain.Entities;

public class ServiceSubscription : EntityBase
{
    public int RemainingUsages { get; set; }
    
    public ObjectId AbonnementId { get; set; }
}