using Application.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Persistence.Repositories;

namespace SportComplexResourceOptimization.Api.Controllers;

[Route("abonnements")]
public class AbonnementsController : BaseController
{
    private readonly IAbonnementService _abonnementService;

    public AbonnementsController(IAbonnementService abonnementService)
    {
        _abonnementService = abonnementService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAbonnement([FromBody] AbonnementCreateDto createDto, string serviceId, string userId,
        CancellationToken cancellationToken)
    {
        await _abonnementService.CreateAbonnement(createDto, serviceId, userId, cancellationToken);
        return Ok();
    }

    [HttpPut("update")]
    public async Task<ActionResult<AbonnementDto>> UpdateAbonnement([FromBody]AbonnementDto abonnementDto, CancellationToken cancellationToken)
    {
        var result = await _abonnementService.UpdateAbonnement(abonnementDto, cancellationToken);
        return Ok(result);
    }
    
    [HttpPut("hide/{abonnementId}")]
    public async Task<ActionResult<AbonnementDto>> HideAbonnement(string abonnementId, CancellationToken cancellationToken)
    {
        var result = await _abonnementService.HideAbonnement(abonnementId, cancellationToken);
        return Ok(result);
    }
    
    [HttpPut("reveal/{abonnementId}")]
    public async Task<ActionResult<AbonnementDto>> RevealAbonnement(string abonnementId, CancellationToken cancellationToken)
    {
        var result = await _abonnementService.RevealAbonnement(abonnementId, cancellationToken);
        return Ok(result);
    }
    
    [HttpDelete]
    public async Task<ActionResult<AbonnementDto>> DeleteAbonnement(string abonnementId, CancellationToken cancellationToken)
    {
        var result = await _abonnementService.DeleteAbonnement(abonnementId, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("all/{serviceId}")]
    public async Task<ActionResult<AbonnementDto>> GetAbonnementPages(int pageNumber, int pageSize, string serviceId,
        CancellationToken cancellationToken)
    {
        var result = await _abonnementService.GetAbonnementPages(pageNumber, pageSize, serviceId, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("visible/{serviceId}")]
    public async Task<ActionResult<AbonnementDto>> GetVisibleAbonnementPages(int pageNumber, int pageSize, string serviceId,
        CancellationToken cancellationToken)
    {
        var result = await _abonnementService.GetVisibleAbonnementPages(pageNumber, pageSize, serviceId, cancellationToken);
        return Ok(result);
    }
}