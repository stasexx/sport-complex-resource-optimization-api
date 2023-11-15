using Application.Models.Dtos;
using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Infrastructure.Services;

public class ReservationService : IReservationService
{
    private IReservationsRepository _reservationsRepository;

    private IEquipmentsSetsRepository _equipmentsSetsRepository;
    

    public ReservationService(IReservationsRepository reservationsRepository, IEquipmentsSetsRepository equipmentsSetsRepository)
    {
        _reservationsRepository = reservationsRepository;
        _equipmentsSetsRepository = equipmentsSetsRepository;
    }

    public async Task<List<Reservation>> GetReservationByTime(DateTime dateTime1, DateTime dateTime2, string equipmentId)
    {
        return  await _reservationsRepository.GetByTime(dateTime1, dateTime2);
    }
    
    public async Task<ReservationCreateDto> CreateReservation(ReservationCreateDto dto, CancellationToken cancellationToken)
    {
        var reservation = new Reservation()
        {
            Duration = dto.Duration,
            StartReservation = dto.StartReservation,
            EndReservation = dto.EndReservation,
            CreatedById = ObjectId.Parse(dto.UserId),
            EquipmentId = ObjectId.Parse(dto.EquipmentId)
        };

        await _reservationsRepository.AddAsync(reservation, cancellationToken);
        return dto;
    }

    public async Task<List<string>> GetAvailableTimeSlots(DateTime startTime, DateTime endTime, int intervalInMinutes, string equipmentId)
    {
        return await _reservationsRepository.GetAvailableTimeSlots(startTime, endTime, intervalInMinutes, equipmentId);
    }

    public async Task<List<ReservationsListCreateDto>> GetAvailableTimeSlotsForEquipment(
        DateTime dateTime1, DateTime dateTime2, int bookingInterval, string equipmentsSetId, CancellationToken cancellationToken)
    {
        var list = await _equipmentsSetsRepository.GetOneAsync(x => x.Id == ObjectId.Parse(equipmentsSetId), cancellationToken);
        List<string> stringIds = list.EquipmentsIds.Select(id => id.ToString()).ToList();
        
        return await _reservationsRepository.GetAvailableTimeSlotsForEquipment(dateTime1, dateTime2, bookingInterval,
            stringIds);
    }
}