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
    
    public async Task<Abonnement> UpdateAbonnementAsync(Abonnement abonnement, CancellationToken cancellationToken)
    {
        var updateDefinition = MongoDB.Driver.Builders<Abonnement>.Update
            .Set(c => c.Name, abonnement.Name)
            .Set(c => c.Duration, abonnement.Duration)
            .Set(c => c.Price, abonnement.Price);
        

        var options = new MongoDB.Driver.FindOneAndUpdateOptions<Abonnement>
        {
            ReturnDocument = ReturnDocument.After
        };

        return await this._collection.FindOneAndUpdateAsync(MongoDB.Driver.Builders<Abonnement>.Filter.Eq(u => u.Id, abonnement.Id), 
            updateDefinition, 
            options, 
            cancellationToken);
    }
    
    public async Task<Abonnement> RevealAbonnementAsync(string abonnementId, CancellationToken cancellationToken)
    {
        var updateDefinition = MongoDB.Driver.Builders<Abonnement>.Update
            .Set(c => c.IsDeleted, false);
        

        var options = new MongoDB.Driver.FindOneAndUpdateOptions<Abonnement>
        {
            ReturnDocument = ReturnDocument.After
        };

        return await this._collection.FindOneAndUpdateAsync(MongoDB.Driver.Builders<Abonnement>.Filter.Eq(u => u.Id, 
                ObjectId.Parse(abonnementId)), 
            updateDefinition, 
            options, 
            cancellationToken);
    }
}