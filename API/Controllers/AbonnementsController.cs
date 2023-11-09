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
    public async Task<IActionResult> CreateAbonnement([FromBody] AbonnementCreateDto createDto, string serviceId,
        CancellationToken cancellationToken)
    {
        await _abonnementService.CreateAbonnement(createDto, serviceId, cancellationToken);
        return Ok();
    }
}