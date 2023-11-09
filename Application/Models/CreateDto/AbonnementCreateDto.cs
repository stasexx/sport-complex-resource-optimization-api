namespace SportComplexResourceOptimizationApi.Application.Models.CreateDto;

public class AbonnementCreateDto
{
    public string? Name { set; get; }

    public double Price { get; set; }

    public int Duration { get; set; }
}