using System.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SportComplexResourceOptimizationApi.Domain.Common;

namespace SportComplexResourceOptimizationApi.Domain.Entities;

public class Reservation : EntityBase
{
    public DateTime StartReservation { get; set; }
    
    public int Duration { get; set; }
    
    public DateTime EndReservation { get; set; }
    
    public ObjectId EquipmentId { get; set; }
}