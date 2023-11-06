using Persistence.Database;
using SportComplexResourceOptimizationApi.Application.IRepository;
using SportComplexResourceOptimizationApi.Entities;

namespace SportComplexResourceOptimizationApi.Persistence.Repositories;

public class UsersRepository : BaseRepository<User>, IUsersRepository
{
    public UsersRepository(MongoDbContext db) : base(db, "Users") { }

}