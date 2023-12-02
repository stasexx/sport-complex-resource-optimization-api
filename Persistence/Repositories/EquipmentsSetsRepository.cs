using Persistence.Database;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Persistence.Repositories;

public class EquipmentsSetsRepository : BaseRepository<EquipmentsSet>, IEquipmentsSetsRepository
{
    public EquipmentsSetsRepository(MongoDbContext db) : base(db, "EquipmentsSets") { }
}