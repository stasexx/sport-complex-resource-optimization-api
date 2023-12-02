using MongoDB.Bson;
using MongoDB.Driver;
using Persistence.Database;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Persistence.Repositories;

public class SensorsRepository : BaseRepository<Sensor>, ISensorsRepository
{
    public SensorsRepository(MongoDbContext db) : base(db, "Sensors") { }

    public async Task<bool> GetStatus(string equipmentId)
    {
        var filter = MongoDB.Driver.Builders<Sensor>.Filter.Eq(x => x.EquipmentId, ObjectId.Parse(equipmentId));
        var sensor = await _collection.Find(filter).FirstOrDefaultAsync();

        if (sensor != null)
        {
            return sensor.Status;
        }
        
        return false;
    }
    public async Task<bool> UpdateStatus(string sensorId, bool newStatus, CancellationToken cancellationToken)
    {
        var filter = MongoDB.Driver.Builders<Sensor>.Filter.Eq(x => x.EquipmentId, ObjectId.Parse(sensorId));
        var update = MongoDB.Driver.Builders<Sensor>.Update
            .Set(x => x.Status, newStatus)
            .Set(x => x.LastModifiedDateUtc, DateTime.UtcNow);

        var result = await _collection.UpdateOneAsync(filter, update);
        
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }
    
    public async Task<Sensor> UpdateEquipment(string sensorId, string newEquipmentId, CancellationToken cancellationToken)
    {
        var updateDefinition = MongoDB.Driver.Builders<Sensor>.Update
            .Set(c => c.EquipmentId, ObjectId.Parse(newEquipmentId));
        

        var options = new MongoDB.Driver.FindOneAndUpdateOptions<Sensor>
        {
            ReturnDocument = ReturnDocument.After
        };

        return await this._collection.FindOneAndUpdateAsync(MongoDB.Driver.Builders<Sensor>.Filter.Eq(u => u.Id, ObjectId.Parse(sensorId)), 
            updateDefinition, 
            options, 
            cancellationToken);
    }
    
}