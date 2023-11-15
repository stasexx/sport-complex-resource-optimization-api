using Application.Models.Statistics;
using MongoDB.Bson;
using MongoDB.Driver;
using Persistence.Database;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Persistence.Repositories;

public class ReservationsRepository : BaseRepository<Reservation>, IReservationsRepository
{
    private readonly IEquipmentsRepository _equipmentsRepository;
    
    public ReservationsRepository(MongoDbContext db, IEquipmentsRepository equipmentsRepository) : base(db,
        "Reservations")
    {
        _equipmentsRepository = equipmentsRepository;
    }

    public async Task<List<Reservation>> GetByTime(DateTime dateTime1, DateTime dateTime2)
    {
        return await this._collection
            .FindAsync(x => x.StartReservation.Hour >= dateTime1.Hour && x.StartReservation.Hour <= dateTime2.Hour).Result
            .ToListAsync();
    }
    
    public async Task<List<Reservation>> GetByDate(DateTime dateTime1, DateTime dateTime2)
    {
        return await this._collection
            .FindAsync(x => x.StartReservation >= dateTime1 && x.StartReservation <= dateTime2).Result
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
    
    public async Task<List<ReservationsListCreateDto>> GetAvailableTimeSlotsForEquipment(
        DateTime dateTime1, DateTime dateTime2, int bookingInterval, List<string> equipmentIds)
    {
        List<ReservationsListCreateDto> availableTimeSlots = new List<ReservationsListCreateDto>();

        foreach (var equipmentId in equipmentIds)
        {
            List<DateTime> timeIntervals = GenerateTimeIntervals(dateTime1, dateTime2, bookingInterval);

            List<Reservation> reservations = await _collection
                .FindAsync(x => x.StartReservation >= dateTime1 && x.EndReservation <= dateTime2 && x.EquipmentId == ObjectId.Parse(equipmentId)).Result
                .ToListAsync();

            foreach (var timeInterval in timeIntervals)
            {
                var overlappingReservation = reservations.FirstOrDefault(r =>
                    r.StartReservation < timeInterval.AddMinutes(bookingInterval) && r.EndReservation > timeInterval);

                if (overlappingReservation == null)
                {
                    if (timeInterval.AddMinutes(bookingInterval) <= dateTime2)
                    {
                        var reservationDto = new ReservationsListCreateDto
                        {
                            EquipmentId = equipmentId,
                            EquipmentName = await _equipmentsRepository.GetEquipmentNameById(ObjectId.Parse(equipmentId)),
                            ReservationTime = timeInterval.ToString("HH:mm") + "-" + timeInterval.AddMinutes(bookingInterval).ToString("HH:mm")
                        };

                        availableTimeSlots.Add(reservationDto);
                    }
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
    
    public async Task<List<ReservationStatisticItem>> GetReservationStatisticsByHour()
    {
        var reservationsByHour = await _collection.Aggregate()
            .Group(r => r.StartReservation.Hour, group => new ReservationStatisticItem
            {
                Hour = group.Key,
                ReservationCount = group.Count()
            })
            .ToListAsync();

        return reservationsByHour;
    }
}