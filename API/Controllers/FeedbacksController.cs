using Microsoft.AspNetCore.Mvc;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;

namespace SportComplexResourceOptimization.Api.Controllers;

[Route("feedback")]
public class FeedbacksController : BaseController
{
    private readonly IFeedbackService _feedbackService;

    public FeedbacksController(IFeedbackService feedbackService)
    {
        _feedbackService = feedbackService;
    }

    [HttpPost("create")]
    public async Task<ActionResult<FeedbackCreateDto>> CreateFeedback([FromBody] FeedbackCreateDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _feedbackService.AddFeedback(dto, cancellationToken);
        return Ok(result);
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<FeedbackCreateDto>> DeleteFeedback([FromBody] FeedbackCreateDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _feedbackService.AddFeedback(dto, cancellationToken);
        return Ok(result);
    }
}