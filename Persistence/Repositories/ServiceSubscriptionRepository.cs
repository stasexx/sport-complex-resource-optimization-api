using MongoDB.Bson;
using MongoDB.Driver;
using Persistence.Database;
using SportComplexResourceOptimizationApi.Domain.Entities;
using SportComplexResourceOptimizationApi.Application.IRepositories;

namespace SportComplexResourceOptimizationApi.Persistence.Repositories;

public class ServiceSubscriptionRepository : BaseRepository<ServiceSubscription>, IServiceSubscriptionsRepository
{
    public ServiceSubscriptionRepository(MongoDbContext db) : base(db, "ServiceSubscriptions") { }

    public async Task UpdateUsages(string id)
    {
        var filter = Builders<ServiceSubscription>.Filter.Eq(x => x.Id, ObjectId.Parse(id));
        var update = Builders<ServiceSubscription>.Update.Inc(x => x.RemainingUsages, -1);

        await _collection.UpdateOneAsync(filter, update);
    }
}