using System.Data.Common;
using MongoDB.Bson;

namespace Application.Models.Dtos;

public class SensorDto
{
    public string? Id { get; set; }
    
    public bool Status { get; set; }
    
    public ObjectId EquipmentId { get; set; }
}