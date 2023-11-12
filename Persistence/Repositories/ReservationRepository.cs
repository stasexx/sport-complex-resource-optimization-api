using MongoDB.Bson;
using MongoDB.Driver;
using Persistence.Database;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Persistence.Repositories;

public class ReservationsRepository : BaseRepository<Reservation>, IReservationsRepository
{
    public ReservationsRepository(MongoDbContext db) : base(db, "Reservations") { }

    public async Task<List<Reservation>> GetByTime(DateTime dateTime1, DateTime dateTime2)
    {
        return await this._collection
            .FindAsync(x => x.StartReservation.Hour >= dateTime1.Hour && x.StartReservation.Hour <= dateTime2.Hour).Result
            .ToListAsync();
    }
    
    public async Task<List<string>> GetAvailableTimeSlots(DateTime dateTime1, DateTime dateTime2, int bookingInterval, string equipmentId)
    {
        List<DateTime> timeIntervals = GenerateTimeIntervals(dateTime1, dateTime2, bookingInterval);

        List<Reservation> reservations = await this._collection
            .FindAsync(x => x.StartReservation >= dateTime1 && x.EndReservation <= dateTime2 && x.EquipmentId == ObjectId.Parse(equipmentId))
            .Result
            .ToListAsync();

        List<string> availableTimeSlots = new List<string>();

        foreach (var timeInterval in timeIntervals)
        {
            var overlappingReservation = reservations.FirstOrDefault(r =>
                r.StartReservation < timeInterval.AddMinutes(bookingInterval) && r.EndReservation > timeInterval);

            if (overlappingReservation == null)
            {
                if (timeInterval.AddMinutes(bookingInterval) <= dateTime2)
                {
                    availableTimeSlots.Add(timeInterval.ToString("HH:mm") + "-" + timeInterval.AddMinutes(bookingInterval).ToString("HH:mm"));
                }
            }
        }

        return availableTimeSlots;
    }

    private List<DateTime> GenerateTimeIntervals(DateTime startTime, DateTime endTime, int intervalInMinutes)
    {
        List<DateTime> timeIntervals = new List<DateTime>();
        var currentTime = startTime;

        while (currentTime < endTime)
        {
            timeIntervals.Add(currentTime);
            currentTime = currentTime.AddMinutes(5);
        }

        return timeIntervals;
    }
}