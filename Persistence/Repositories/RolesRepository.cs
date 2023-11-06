using Persistence.Database;
using SportComplexResourceOptimizationApi.Application.IRepository;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Persistence.Repositories;

public class RolesRepository : BaseRepository<Role>, IRolesRepository
{
    public RolesRepository(MongoDbContext db) : base(db, "Roles") { }

}