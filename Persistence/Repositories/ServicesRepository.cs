using MongoDB.Bson;
using Persistence.Database;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Domain.Entities;
using ReturnDocument = MongoDB.Driver.ReturnDocument;

namespace SportComplexResourceOptimizationApi.Persistence.Repositories;

public class ServicesRepository : BaseRepository<Service>, IServicesRepository
{
    public ServicesRepository(MongoDbContext db) : base(db, "Services") { }
    
    public async Task<Service> UpdateServiceAsync(Service service, CancellationToken cancellationToken)
    {
        var updateDefinition = MongoDB.Driver.Builders<Service>.Update
            .Set(c => c.Name, service.Name);
        

        var options = new MongoDB.Driver.FindOneAndUpdateOptions<Service>
        {
            ReturnDocument = ReturnDocument.After
        };

        return await this._collection.FindOneAndUpdateAsync(MongoDB.Driver.Builders<Service>.Filter.Eq(u => u.Id, service.Id), 
            updateDefinition, 
            options, 
            cancellationToken);
    }
    
    public async Task<Service> RevealServiceAsync(string serviceId, CancellationToken cancellationToken)
    {
        var updateDefinition = MongoDB.Driver.Builders<Service>.Update
            .Set(c => c.IsDeleted, false);
        

        var options = new MongoDB.Driver.FindOneAndUpdateOptions<Service>
        {
            ReturnDocument = ReturnDocument.After
        };

        return await this._collection.FindOneAndUpdateAsync(MongoDB.Driver.Builders<Service>.Filter.Eq(u => u.Id, 
                ObjectId.Parse(serviceId)), 
            updateDefinition, 
            options, 
            cancellationToken);
    }
}