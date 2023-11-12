using Persistence.Database;

namespace SmartInventorySystemApi.IntegrationTests;

public class DbInitializer
{
    private readonly MongoDbContext _dbContext;

    public DbInitializer(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void InitializeDb()
    {
        _dbContext.Client.DropDatabase(_dbContext.Db.DatabaseNamespace.DatabaseName);
    }
}