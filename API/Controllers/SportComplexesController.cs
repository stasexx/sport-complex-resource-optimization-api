using Application.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Models.Dtos;
using SportComplexResourceOptimizationApi.Application.Models.Identity;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;
using SportComplexResourceOptimizationApi.Application.Paging;

namespace SportComplexResourceOptimization.Api.Controllers;

[Route("sportcomplexes")]
public class SportComplexesController : BaseController 
{
    private readonly ISportComplexesService _sportComplexesService;

    public SportComplexesController(ISportComplexesService sportComplexesService)
    {
        _sportComplexesService = sportComplexesService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSportComplex([FromBody] SportComplexCreateDto createDto, string ownerId,
        CancellationToken cancellationToken)
    {
        await _sportComplexesService.CreateSportComplex(createDto, ownerId, cancellationToken);
        return Ok();
    }
    
}