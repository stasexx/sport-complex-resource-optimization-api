using Application.Models.Statistics;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Application.IRepositories;

public interface IReservationsRepository : IBaseRepository<Reservation>
{
    Task<List<Reservation>> GetByTime(DateTime dateTime1, DateTime dateTime2);

    Task<List<Reservation>> GetByDate(DateTime dateTime1, DateTime dateTime2);

    Task<List<string>> GetAvailableTimeSlots(DateTime dateTime1, DateTime dateTime2, int bookingInterval,
        string equipmentId);

    Task<List<ReservationStatisticItem>> GetReservationStatisticsByHour();

    Task<List<ReservationsListCreateDto>> GetAvailableTimeSlotsForEquipment(
        DateTime dateTime1, DateTime dateTime2, int bookingInterval, List<string> equipmentIds);
}