namespace Application.Models.Statistics;

public class UsageStatistics
{
    public string EquipmentId { get; set; }
    
    public string EquipmentName { get; set; }
    
    public TimeSpan TotalUsageTime { get; set; }
}