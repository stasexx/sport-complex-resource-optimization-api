using MongoDB.Bson;
using MongoDB.Driver;
using Persistence.Database;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Persistence.Repositories;

public class UsagesHistoryRepository : BaseRepository<UsagesHistory>, IUsagesHistoryRepository
{
    public UsagesHistoryRepository(MongoDbContext db) : base(db, "UsagesHistories") { }

    public async Task UpdateUsages(string id)
    {
        var filter = Builders<UsagesHistory>.Filter.Eq(x => x.EquipmentId, ObjectId.Parse(id));

        var existingRecord = await _collection.Find(filter).FirstOrDefaultAsync();

        if (existingRecord != null)
        {
            int currentUsages = existingRecord.TotalUsages;
            int newUsages = currentUsages + 1;
            var update = Builders<UsagesHistory>.Update.Set(x => x.TotalUsages, newUsages);

            await _collection.UpdateOneAsync(filter, update);
        }
    }
}