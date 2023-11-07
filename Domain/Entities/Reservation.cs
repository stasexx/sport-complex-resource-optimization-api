using System.Data;
using SportComplexResourceOptimizationApi.Domain.Common;

namespace SportComplexResourceOptimizationApi.Domain.Entities;

public class Reservation : EntityBase
{
    public DateTime StartReservation { get; set; }
    
    public DateTime EndReservation { get; set; }
}