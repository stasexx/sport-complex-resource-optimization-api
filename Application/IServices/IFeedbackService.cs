using SportComplexResourceOptimizationApi.Application.Models.CreateDto;

namespace SportComplexResourceOptimizationApi.Application.IServices;

public interface IFeedbackService
{
    Task<FeedbackCreateDto> AddFeedback(FeedbackCreateDto dto, CancellationToken cancellationToken);
}