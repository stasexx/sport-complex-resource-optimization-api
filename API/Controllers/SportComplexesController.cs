using Application.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;
using SportComplexResourceOptimizationApi.Domain.Entities;


namespace SportComplexResourceOptimization.Api.Controllers;

[Route("sportcomplexes")]
public class SportComplexesController : BaseController 
{
    private readonly ISportComplexesService _sportComplexesService;

    public SportComplexesController(ISportComplexesService sportComplexesService)
    {
        _sportComplexesService = sportComplexesService;
    }
    
    [HttpPost("create/{ownerId}")]
    public async Task<ActionResult<SportComplexDto>> CreateSportComplex([FromBody] SportComplexCreateDto createDto, string ownerId,
        CancellationToken cancellationToken)
    {
        var result = await _sportComplexesService.CreateSportComplex(createDto, ownerId, cancellationToken);
        return Ok(result);
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> UpdateSportComplex([FromBody] SportComplexUpdateDto updateDto,
        CancellationToken cancellationToken)
    {
        var result = await _sportComplexesService.UpdateSportComplex(updateDto, cancellationToken);
        return Ok(result);
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteSportComplex(string sportComplexId, CancellationToken cancellationToken)
    {
        var result = await _sportComplexesService.DeleteSportComplex(sportComplexId, cancellationToken);
        return Ok(result);
    }
    
    [HttpPut("hide/{sportComplexId}")]
    public async Task<IActionResult> HideSportComplex(string sportComplexId, CancellationToken cancellationToken)
    {
        var result = await _sportComplexesService.HideSportComplex(sportComplexId, cancellationToken);
        return Ok(result);
    }
    
    [HttpPut("reveal/{sportComplexId}")]
    public async Task<IActionResult> RevealSportComplex(string sportComplexId, CancellationToken cancellationToken)
    {
        var result = await _sportComplexesService.RevealSportComplex(sportComplexId, cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetVisibleSportComplexesPages(int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var result =
            await _sportComplexesService.GetVisibleSportComplexesPages(pageNumber, pageSize, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetSportComplexesPages(int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var result =
            await _sportComplexesService.GetSportComplexesPages(pageNumber, pageSize, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<SportComplexDto>> GetUserAsync(string id, CancellationToken cancellationToken)
    {
        var user = await _sportComplexesService.GetSportComplexAsync(id, cancellationToken);
        return Ok(user);
    }
    
    [HttpGet("search/{name}")]
    public async Task<IActionResult> GetVisibleSportComplexesByNamePages(int pageNumber, int pageSize, string name,
        CancellationToken cancellationToken)
    {
        var result =
            await _sportComplexesService.GetVisibleSportComplexesByNamePages(pageNumber, pageSize,name, cancellationToken);
        return Ok(result);
    }
    
}