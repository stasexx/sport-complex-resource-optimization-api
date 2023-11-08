using Persistence.Database;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Persistence.Repositories;

public class ServicesRepository : BaseRepository<Service>, IServicesRepository
{
    public ServicesRepository(MongoDbContext db) : base(db, "Services") { }
}