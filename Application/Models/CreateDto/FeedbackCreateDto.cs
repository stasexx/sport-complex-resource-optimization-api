namespace SportComplexResourceOptimizationApi.Application.Models.CreateDto;

public class FeedbackCreateDto
{
    public string SportComplexId { get; set; }
    
    public string UserId { get; set; }
    
    public string Description { get; set; }
    
    public double Rating { get; set; }
}