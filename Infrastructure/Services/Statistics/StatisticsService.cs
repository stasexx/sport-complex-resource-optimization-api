using Application.Models.Statistics;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.IServices.StatisticsService;

namespace SportComplexResourceOptimizationApi.Infrastructure.Services.Statistics;

public class StatisticsService : IStatisticsService
{
    private readonly IReservationsRepository _reservationsRepository;

    private readonly IEquipmentsRepository _equipmentsRepository;

    public StatisticsService(IEquipmentsRepository equipmentsRepository, IReservationsRepository reservationsRepository)
    {
        _reservationsRepository = reservationsRepository;
        _equipmentsRepository = equipmentsRepository;
    }
    
    public async Task<List<EquipmentUsageStatisticsDto>> GetEquipmentUsageStatistics(DateTime startDate, DateTime endDate)
    {
        var reservations = await _reservationsRepository.GetByDate(startDate, endDate);
        var equipmentIds = reservations.Select(r => r.EquipmentId).Distinct();

        var equipmentStatistics = new List<EquipmentUsageStatisticsDto>();

        foreach (var equipmentId in equipmentIds)
        {
            var equipmentName = await _equipmentsRepository.GetEquipmentNameById(equipmentId);
            var reservationCount = reservations.Count(r => r.EquipmentId == equipmentId);

            equipmentStatistics.Add(new EquipmentUsageStatisticsDto
            {
                EquipmentName = equipmentName,
                ReservationCount = reservationCount
            });
        }

        return equipmentStatistics;
    }

    public async Task<List<ReservationStatisticItem>> GetReservationStatisticsByHour()
    {
        return await _reservationsRepository.GetReservationStatisticsByHour();
    }
}