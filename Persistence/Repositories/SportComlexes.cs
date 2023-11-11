using MongoDB.Driver;
using Persistence.Database;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Domain.Entities;
using ReturnDocument = MongoDB.Driver.ReturnDocument;

namespace SportComplexResourceOptimizationApi.Persistence.Repositories;

public class SportComplexesRepository : BaseRepository<SportComplex>, ISportComplexesRepository
{
    public SportComplexesRepository(MongoDbContext db) : base(db, "SportComplexes") { }
    
    public async Task<SportComplex> UpdateSportComplexAsync(SportComplex sportComplex, CancellationToken cancellationToken)
    {
        var updateDefinition = Builders<SportComplex>.Update
            .Set(c => c.Name, sportComplex.Name)
            .Set(c => c.Address, sportComplex.Address)
            .Set(c => c.Description, sportComplex.Description)
            .Set(c => c.Email, sportComplex.Email)
            .Set(c => c.Rating, sportComplex.Rating)
            .Set(u => u.LastModifiedDateUtc, sportComplex.LastModifiedDateUtc)
            .Set(u => u.LastModifiedById, sportComplex.LastModifiedById);
        

        var options = new FindOneAndUpdateOptions<SportComplex>
        {
            ReturnDocument = ReturnDocument.After
        };

        return await this._collection.FindOneAndUpdateAsync(
            Builders<SportComplex>.Filter.Eq(u => u.Id, sportComplex.Id), 
            updateDefinition, 
            options, 
            cancellationToken);
    }

}