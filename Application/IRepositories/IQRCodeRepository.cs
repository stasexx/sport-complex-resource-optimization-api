using MongoDB.Bson;
using SportComplexResourceOptimizationApi.Domain.Entities.Identity;
using SportComplexResourceOptimizationApi.Entities;

namespace SportComplexResourceOptimizationApi.Application.IRepository;

public interface IQRCodeRepository : IBaseRepository<QRCodeData>
{
    
}