namespace SportComplexResourceOptimizationApi.Application.Models.CreateDto;

public class EquipmentsSetCreateDto
{
    public string Name { get; set; }
    
    public string UserId { get; set; }
    
    public List<string> Equipments { get; set; }
}