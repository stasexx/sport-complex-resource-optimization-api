using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Infrastructure.Services;

public class FeedbacksService : IFeedbackService
{
    private readonly IFeedbacksRepository _feedbacksRepository;

    private readonly ISportComplexesRepository _sportComplexesRepository;

    public FeedbacksService(IFeedbacksRepository feedbacksRepository, ISportComplexesRepository sportComplexesRepository)
    {
        _feedbacksRepository = feedbacksRepository;
        _sportComplexesRepository = sportComplexesRepository;
    }

    public async Task<FeedbackCreateDto> AddFeedback(FeedbackCreateDto dto, CancellationToken cancellationToken)
    {
        var feedback = new Feedback()
        {
            CreatedById = ObjectId.Parse(dto.UserId),
            SportComplexId = ObjectId.Parse(dto.SportComplexId),
            Description = dto.Description,
            Rating = dto.Rating,
            CreatedDateUtc = DateTime.UtcNow,
        };

        await _feedbacksRepository.AddAsync(feedback, cancellationToken);
        
        var rating = await _feedbacksRepository.UpdateSportComplexRating(ObjectId.Parse(dto.SportComplexId));

        await _sportComplexesRepository.UpdateRating(ObjectId.Parse(dto.SportComplexId), rating, cancellationToken);

        return dto;
    }
}