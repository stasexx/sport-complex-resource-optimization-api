using Microsoft.AspNetCore.Mvc;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;

namespace SportComplexResourceOptimization.Api.Controllers;

[Route("servsubs")]
public class ServiceSubscriptionsController : BaseController
{
    private readonly IServiceSubscriptionService _serviceSubscriptionService;

    public ServiceSubscriptionsController(IServiceSubscriptionService serviceSubscriptionService)
    {
        _serviceSubscriptionService = serviceSubscriptionService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateServiceSubscription([FromBody] ServiceSubscriptionCreateDto createDto, string userId, string abbId,
        CancellationToken cancellationToken)
    {
        await _serviceSubscriptionService.CreateAbonnement(createDto, userId, abbId, cancellationToken);
        return Ok();
    }
    
    [HttpPost("update")]
    public async Task<IActionResult> UpdateUsages(string id, CancellationToken cancellationToken)
    {
        await _serviceSubscriptionService.UpdateUsages(id, cancellationToken);
        return Ok();
    }
}