using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SportComplexResourceOptimizationApi.Domain.Common;

namespace SportComplexResourceOptimizationApi.Domain.Entities.Identity;

public class QRCodeData : EntityBase
{
    public byte[] QRCodeBytes { get; set; }

}