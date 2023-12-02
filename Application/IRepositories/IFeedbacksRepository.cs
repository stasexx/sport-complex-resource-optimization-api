using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Application.IRepositories;

public interface IFeedbacksRepository : IBaseRepository<Feedback>
{
    Task<double> UpdateSportComplexRating(ObjectId sportComplexId);
}