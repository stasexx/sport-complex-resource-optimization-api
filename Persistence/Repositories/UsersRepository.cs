using MongoDB.Bson;
using Persistence.Database;
using System.Linq.Expressions;
using SportComplexResourceOptimizationApi.Application.IRepository;
using SportComplexResourceOptimizationApi.Entities;

namespace SportComplexResourceOptimization.Persistence.Repositories;

public class UsersRepository : BaseRepository<User>, IUsersRepository
{
    public UsersRepository(MongoDbContext db) : base(db, "Users") { }

}