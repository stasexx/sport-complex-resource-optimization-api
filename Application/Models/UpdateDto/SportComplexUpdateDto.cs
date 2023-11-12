namespace SportComplexResourceOptimizationApi.Application.Models.UpdateDto;

public class SportComplexUpdateDto
{
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string Address { get; set; }
    
    public string Description { get; set; }
    
    public double Rating { get; set; }
}