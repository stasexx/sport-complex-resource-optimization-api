using MongoDB.Bson;
using MongoDB.Driver;
using Persistence.Database;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Persistence.Repositories;

public class AbonnementRepository : BaseRepository<Abonnement>, IAbonnementsRepository
{
    public AbonnementRepository(MongoDbContext db) : base(db, "Abonnements") { }

    public async Task<int> GetDuration(string id)
    {
        var abonnement = await _collection.FindAsync(x => x.Id == ObjectId.Parse(id)).Result.FirstOrDefaultAsync();
        return abonnement.Duration;
    }
}