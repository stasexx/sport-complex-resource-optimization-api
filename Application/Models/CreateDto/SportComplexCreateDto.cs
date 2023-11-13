using MongoDB.Bson;

namespace SportComplexResourceOptimizationApi.Application.Models.CreateDto;

public class SportComplexCreateDto
{
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string City { get; set; }
    
    public string Address { get; set; }
    
}