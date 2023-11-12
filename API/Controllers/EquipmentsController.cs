using Application.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;
using SportComplexResourceOptimizationApi.Application.Paging;

namespace SportComplexResourceOptimization.Api.Controllers;

[Route("equipments")]
public class EquipmentsController : BaseController
{
    private readonly IEquipmentService _equipmentService;

    public EquipmentsController(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateEquipment([FromBody] EquipmentDto createDto, string serviceId, string userId,
        CancellationToken cancellationToken)
    {
        await _equipmentService.CreateEquipment(createDto, serviceId, userId, cancellationToken);
        return Ok();
    }

    [HttpPut("update")]
    public async Task<ActionResult<EquipmentDto>> UpdateEquipment([FromBody] EquipmentUpdateDto equipmentUpdateDto,
        CancellationToken cancellationToken)
    {
        var result = await _equipmentService.UpdateEquipment(equipmentUpdateDto, cancellationToken);
        return Ok(result);
    }

    [HttpPut("hide/{equipmentId}")]
    public async Task<ActionResult<EquipmentDto>> HideEquipmentAsync(string equipmentId, CancellationToken cancellationToken)
    {
        var result = await _equipmentService.HideEquipment(equipmentId, cancellationToken);
        return Ok(result);
    }
    
    [HttpPut("reveal/{equipmentId}")]
    public async Task<ActionResult<EquipmentDto>> RevealEquipmentAsync(string equipmentId, CancellationToken cancellationToken)
    {
        var result = await _equipmentService.RevealEquipment(equipmentId, cancellationToken);
        return Ok(result);
    }

    [HttpGet("visible/{serviceId}")]
    public async Task<ActionResult<PagedList<EquipmentDto>>> GetVisibleEquipmentPages(int pageNumber, int pageSize,
        string serviceId, CancellationToken cancellationToken)
    {
        var result =
            await _equipmentService.GetVisibleEquipmentPages(pageNumber, pageSize, serviceId, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("all/{serviceId}")]
    public async Task<ActionResult<PagedList<EquipmentDto>>> GetEquipmentPages(int pageNumber, int pageSize,
        string serviceId, CancellationToken cancellationToken)
    {
        var result =
            await _equipmentService.GetEquipmentPages(pageNumber, pageSize, serviceId, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("status/{equipmentId}")]
    public async Task<ActionResult<bool>> GetStatus(string equipmentId, CancellationToken cancellationToken)
    {
        var result = await _equipmentService.GetStatus(equipmentId, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("delete/{equipmentId}")]
    public async Task<ActionResult<EquipmentDto>> DeleteEquipment(string equipmentId,
        CancellationToken cancellationToken)
    {
        var result = await _equipmentService.DeleteEquipment(equipmentId, cancellationToken);
        return Ok(result);
    }
    
    
}