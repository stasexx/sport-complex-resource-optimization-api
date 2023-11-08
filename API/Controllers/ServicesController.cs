using Microsoft.AspNetCore.Mvc;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Persistence.Repositories;

namespace SportComplexResourceOptimization.Api.Controllers;

[Route("services")]
public class ServicesController : BaseController
{
    private readonly IServiceService _serviceService;

    public ServicesController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateService([FromBody] ServiceCreateDto createDto, string complexId,
        CancellationToken cancellationToken)
    {
        await _serviceService.CreateService(createDto, complexId, cancellationToken);
        return Ok();
    }
}