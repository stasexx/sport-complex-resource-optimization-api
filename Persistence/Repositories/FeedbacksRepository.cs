using MongoDB.Bson;
using MongoDB.Driver;
using Persistence.Database;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Persistence.Repositories;

public class FeedbacksRepository : BaseRepository<Feedback>, IFeedbacksRepository
{
    public FeedbacksRepository(MongoDbContext db) : base(db, "Feedbacks")
    {
    }

    public async Task<double> UpdateSportComplexRating(ObjectId sportComplexId)
    {
        var filter = Builders<Feedback>.Filter.Eq(x => x.SportComplexId, sportComplexId);
        var feedbacks = await _collection.Find(filter).ToListAsync();

        if (feedbacks.Count > 0)
        {
            var averageRating = feedbacks.Average(x => x.Rating);
            return averageRating;
        }
        
        return 0;
    }
}