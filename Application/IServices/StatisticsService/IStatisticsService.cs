using Application.Models.Statistics;

namespace SportComplexResourceOptimizationApi.Application.IServices.StatisticsService;

public interface IStatisticsService
{
    Task<List<EquipmentUsageStatisticsDto>> GetEquipmentUsageStatistics(DateTime startDate, DateTime endDate);

    Task<List<ReservationStatisticItem>> GetReservationStatisticsByHour();

    Task<List<UsageStatistics>> GetUserEquipmentUsageStatistics(string userId, CancellationToken cancellationToken);
}