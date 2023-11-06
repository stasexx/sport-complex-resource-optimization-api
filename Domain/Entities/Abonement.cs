using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Domain.Common;

namespace SportComplexResourceOptimizationApi.Domain.Entities;

public class Abonement : EntityBase
{
    public string? Name { set; get; }

    public double Price { get; set; }

    public int Duration { get; set; }

    public ObjectId UserId { get; set; }

}