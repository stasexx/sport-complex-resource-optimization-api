using Application.Models.Statistics;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Application.IServices;

public interface IReservationService
{
    Task<List<Reservation>> GetReservationByTime(DateTime dateTime1, DateTime dateTime2, string equipmentId);

    Task<ReservationCreateDto> CreateReservation(ReservationCreateDto dto, CancellationToken cancellationToken);

    Task<List<string>> GetAvailableTimeSlots(DateTime startTime, DateTime endTime, int intervalInMinutes, string equipmentId);
    
    Task<List<ReservationsListCreateDto>> GetAvailableTimeSlotsForEquipment(
        DateTime dateTime1, DateTime dateTime2, int bookingInterval, string equipmentsSetId,
        CancellationToken cancellationToken);

}