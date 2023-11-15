using Microsoft.AspNetCore.Mvc;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Persistence.Repositories;

namespace SportComplexResourceOptimization.Api.Controllers;

[Route("reservations")]
public class ReservationController : BaseController
{
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public async Task<ActionResult> GetReservationsByTime(DateTime dateTime1, DateTime dateTime2, string id)
    {
        var result = await _reservationService.GetReservationByTime(dateTime1, dateTime2, id);
        return Ok(result);
    }
    
    [HttpGet("slots")]
    public async Task<List<string>> GetAvailableTimeSlots(DateTime dateTime1, DateTime dateTime2, int intervalInMinutes, string equipmentId)
    {
        return await _reservationService.GetAvailableTimeSlots(dateTime1, dateTime2, intervalInMinutes, equipmentId);
    }
    
    [HttpGet("equipmentsslots")]
    public async Task<List<ReservationsListCreateDto>> GetAvailableTimeSlotsForEquipments(
        DateTime dateTime1, DateTime dateTime2, int intervalInMinutes, string setId, CancellationToken cancellationToken)
    {
        return await _reservationService.GetAvailableTimeSlotsForEquipment(dateTime1, dateTime2, intervalInMinutes, setId, cancellationToken);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateReservations([FromBody] ReservationCreateDto dto, CancellationToken cancellationToken)
    {
        var result = await _reservationService.CreateReservation(dto, cancellationToken);
        return Ok(result);
    }
    
    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteReservations([FromBody] ReservationCreateDto dto, CancellationToken cancellationToken)
    {
        var result = await _reservationService.CreateReservation(dto, cancellationToken);
        return Ok(result);
    }
}