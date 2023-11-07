using SportComplexResourceOptimizationApi.Domain.Common;

namespace SportComplexResourceOptimizationApi.Domain.Entities;

public class SportComplex : EntityBase 
{
    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? OperatingHours { get; set; }

    public double Rating { get; set; }
}