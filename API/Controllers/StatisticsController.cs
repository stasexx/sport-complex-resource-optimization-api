using Application.Models.Statistics;
using Microsoft.AspNetCore.Mvc;
using SportComplexResourceOptimizationApi.Application.IServices.StatisticsService;
using SportComplexResourceOptimizationApi.Persistence.Repositories;

namespace SportComplexResourceOptimization.Api.Controllers;

[Route("statistics")]
public class StatisticsController : BaseController
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }
    
    [HttpGet("equipment")]
    public async Task<ActionResult<List<EquipmentUsageStatisticsDto>>> GetEquipmentUsages(DateTime startDate, DateTime endDate)
    {
        var result = await _statisticsService.GetEquipmentUsageStatistics(startDate, endDate);
        return result;
    }
    
    [HttpGet("reservations/hours")]
    public async Task<ActionResult<List<ReservationStatisticItem>>> GetReservationStatisticsByHour()
    {
        var result = await _statisticsService.GetReservationStatisticsByHour();
        return result;
        
    }
    
    [HttpGet("usages/{userId}")]
    public async Task<List<UsageStatistics>> GetUserEquipmentUsageStatistics(string userId, CancellationToken cancellationToken)
    {
        var result = await _statisticsService.GetUserEquipmentUsageStatistics(userId, cancellationToken);
        return result;
    }
}