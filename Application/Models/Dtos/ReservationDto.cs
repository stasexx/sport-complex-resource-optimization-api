using MongoDB.Bson;

namespace Application.Models.Dtos;

public class ReservationDto
{
    public DateTime StartReservation { get; set; }
    
    public int Duration { get; set; }
    
    public DateTime EndReservation { get; set; }
    
    public ObjectId EquipmentId { get; set; }
}