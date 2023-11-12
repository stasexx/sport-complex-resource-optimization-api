using Application.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;
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
    public async Task<IActionResult> CreateService([FromBody] ServiceCreateDto createDto, string userId,
        CancellationToken cancellationToken)
    {
        await _serviceService.CreateService(createDto, userId, cancellationToken);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateService(ServiceUpdateDto service, CancellationToken cancellationToken)
    {
        var result = await _serviceService.UpdateService(service, cancellationToken);
        return Ok(result);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteService(string serviceId, CancellationToken cancellationToken)
    {
        var result = await _serviceService.DeleteService(serviceId, cancellationToken);
        return Ok(result);
    }
    
    [HttpPut("hide/{serviceId}")]
    public async Task<IActionResult> HideService(string serviceId, CancellationToken cancellationToken)
    {
        var result = await _serviceService.HideService(serviceId, cancellationToken);
        return Ok(result);
    }
    
    [HttpPut("reveal/{serviceId}")]
    public async Task<IActionResult> RevealService(string serviceId, CancellationToken cancellationToken)
    {
        var result = await _serviceService.RevealService(serviceId, cancellationToken);
        return Ok(result);
    }
    
}