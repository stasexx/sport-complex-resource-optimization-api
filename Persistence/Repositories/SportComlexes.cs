using Persistence.Database;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Domain.Entities;

namespace SportComplexResourceOptimizationApi.Persistence.Repositories;

public class SportComplexesRepository : BaseRepository<SportComplex>, ISportComplexesRepository
{
    public SportComplexesRepository(MongoDbContext db) : base(db, "SportComplexes") { }

}