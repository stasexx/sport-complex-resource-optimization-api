using Application.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using SportComplexResourceOptimizationApi.Application.IServices;

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
    public async Task<IActionResult> CreateEquipment([FromBody] EquipmentDto createDto, string complexId,
        CancellationToken cancellationToken)
    {
        await _equipmentService.CreateEquipment(createDto, complexId, cancellationToken);
        return Ok();
    }
}