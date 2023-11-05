using SportComplexResourceOptimizationApi.Domain.Common;

namespace SportComplexResourceOptimizationApi.Domain.Entities;

public class Equipment : EntityBase 
{
    public string Name { get; set; }

    public string Loation { get; set; }
}