using Persistence.Database;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Persistence.Repositories;

public class EquipmentsRepository : BaseRepository<Equipment>, IEquipmentsRepository
{
    public EquipmentsRepository(MongoDbContext db) : base(db, "Equipments") { }
}