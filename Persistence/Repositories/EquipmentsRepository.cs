using MongoDB.Bson;
using Persistence.Database;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Domain.Entities;
using ReturnDocument = MongoDB.Driver.ReturnDocument;

namespace SportComplexResourceOptimizationApi.Persistence.Repositories;

public class EquipmentsRepository : BaseRepository<Equipment>, IEquipmentsRepository
{
    public EquipmentsRepository(MongoDbContext db) : base(db, "Equipments") { }
    
    public async Task<Equipment> UpdateEquipmentAsync(Equipment equipment, CancellationToken cancellationToken)
    {
        var updateDefinition = MongoDB.Driver.Builders<Equipment>.Update
            .Set(c => c.Name, equipment.Name);
        

        var options = new MongoDB.Driver.FindOneAndUpdateOptions<Equipment>
        {
            ReturnDocument = ReturnDocument.After
        };

        return await this._collection.FindOneAndUpdateAsync(MongoDB.Driver.Builders<Equipment>.Filter.Eq(u => u.Id, equipment.Id), 
            updateDefinition, 
            options, 
            cancellationToken);
    }
    
    public async Task<Equipment> RevealEquipmentAsync(string equipmentId, CancellationToken cancellationToken)
    {
        var updateDefinition = MongoDB.Driver.Builders<Equipment>.Update
            .Set(c => c.IsDeleted, false);
        

        var options = new MongoDB.Driver.FindOneAndUpdateOptions<Equipment>
        {
            ReturnDocument = ReturnDocument.After
        };

        return await this._collection.FindOneAndUpdateAsync(MongoDB.Driver.Builders<Equipment>.Filter.Eq(u => u.Id, 
                ObjectId.Parse(equipmentId)), 
            updateDefinition, 
            options, 
            cancellationToken);
    }
}