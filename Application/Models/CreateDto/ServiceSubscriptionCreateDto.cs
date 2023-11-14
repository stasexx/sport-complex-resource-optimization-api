using MongoDB.Bson;

namespace SportComplexResourceOptimizationApi.Application.Models.CreateDto;

public class ServiceSubscriptionCreateDto
{
    public int RemainingUsages { get; set; }
    
    public string AbonnementId { get; set; }
}