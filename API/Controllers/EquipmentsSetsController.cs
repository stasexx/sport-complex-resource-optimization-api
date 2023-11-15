using Microsoft.AspNetCore.Mvc;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;

namespace SportComplexResourceOptimization.Api.Controllers;

[Route("sets")]
public class EquipmentsSetsController : BaseController
{
    private readonly IEquipmentsSetsService _equipmentsSetsService;

    public EquipmentsSetsController(IEquipmentsSetsService equipmentsSetsService)
    {
        _equipmentsSetsService = equipmentsSetsService;
    }

    [HttpPost("create")]
    public async Task<ActionResult<EquipmentsSetCreateDto>> CreateNewSet([FromBody]EquipmentsSetCreateDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _equipmentsSetsService.CreateNewSet(dto, cancellationToken);
        return Ok(result);
    }
}