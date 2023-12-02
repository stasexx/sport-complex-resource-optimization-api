using Application.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;

namespace SportComplexResourceOptimization.Api.Controllers;

[Route("sensors")]
public class SensorsController : BaseController
{
    private readonly ISensorService _sensorService;

    public SensorsController(ISensorService sensorService)
    {
        _sensorService = sensorService;
    }

    [HttpPost("create")]
    public async Task<ActionResult<SensorCreateDto>> CreateSensor([FromBody] SensorCreateDto sensorCreateDto, CancellationToken cancellationToken)
    {
        var result = await _sensorService.CreateSensor(sensorCreateDto, cancellationToken);
        return Ok(result);
    }
    
    [HttpPut("update")]
    public async Task<ActionResult<ServiceUpdateDto>> UpdateSensor([FromBody] SensorUpdateDto sensorUpdateDto, CancellationToken cancellationToken)
    {
        var result = await _sensorService.UpdateStatus(sensorUpdateDto, cancellationToken);
        return Ok(result);
    }
    
    [HttpPut("update/sensorId={sensorId}/equipmentId={equipmentId}")]
    public async Task<ActionResult<ServiceUpdateDto>> UpdateEquipment(string sensorId, string equipmentId, CancellationToken cancellationToken)
    {
        var result = await _sensorService.UpdateEquipment(sensorId, equipmentId, cancellationToken);
        return Ok(result);
    }
}