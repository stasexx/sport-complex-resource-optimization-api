using MongoDB.Bson;

namespace SportComplexResourceOptimizationApi.Application.Models.CreateDto;

public class ReservationCreateDto
{
    public DateTime StartReservation { get; set; }
    
    public int Duration { get; set; }
    
    public DateTime EndReservation { get; set; }
    
    public string EquipmentId { get; set; }
    
    public string UserId { get; set; }
}