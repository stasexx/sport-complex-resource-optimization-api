using MongoDB.Bson;
using MongoDB.Driver;
using Persistence.Database;
using SportComplexResourceOptimizationApi.Application.IRepository;
using SportComplexResourceOptimizationApi.Domain.Entities.Identity;
using SportComplexResourceOptimizationApi.Entities;

namespace SportComplexResourceOptimizationApi.Persistence.Repositories;

public class QRCodeRepository : BaseRepository<QRCodeData>, IQRCodeRepository
{
    public QRCodeRepository(MongoDbContext db) : base(db, "QRCodes") { }

    public void InsertQRCode(QRCodeData qrCodeData)
    {
        this._collection.InsertOne(qrCodeData);
    }

    public QRCodeData GetQRCodeById(string id)
    {
        var objectId = new ObjectId(id);
        return this._collection.Find(q => q.Id == objectId).FirstOrDefault();
    }

}