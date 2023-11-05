using System.Data;

namespace SportComplexResourceOptimizationApi.Domain.Entities;

public class Reservation
{
    public DateTime ReservationDate { get; set; }

    public int Time { get; set; }
}