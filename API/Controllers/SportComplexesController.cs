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
    private readonly ISportComlexesService _sportComlexesService;

    public SportComplexesController(ISportComlexesService sportComlexesService)
    {
        _sportComlexesService = sportComlexesService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUserAsync(string id, CancellationToken cancellationToken)
    {
        var imageUrls = _sportComlexesService.GetSportComplexImages(id);
        return Ok(imageUrls);
    }

}