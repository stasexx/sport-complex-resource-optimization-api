using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Persistence.Database;
using SportComplexResourceOptimizationApi.Domain.Entities;
using SportComplexResourceOptimizationApi.Infrastructure.Services.Identity;

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
        InitializeUsersAsync().Wait();
    }
    
    public async Task InitializeUsersAsync()
    {
        #region Roles

        var rolesCollection = _dbContext.Db.GetCollection<Role>("Roles");

        var userRole = new Role
        {
            Name = "User"
        };
        await rolesCollection.InsertOneAsync(userRole);

        var ownerRole = new Role
        {
            Name = "Owner"
        };
        await rolesCollection.InsertOneAsync(ownerRole);

        var adminRole = new Role
        {
            Name = "Admin"
        };
        await rolesCollection.InsertOneAsync(adminRole);

        #endregion

        #region Users

        var passwordHasher = new PasswordHasher(new Logger<PasswordHasher>(new LoggerFactory()));
        var usersCollection = _dbContext.Db.GetCollection<User>("Users");

        var testUser = new User
        {
            Id = ObjectId.Parse("652c3b89ae02a3135d6409fc"),
            Email = "test@gmail.com",
            Phone = "+380123456789",
            Roles = new List<Role> { userRole },
            PasswordHash = passwordHasher.Hash("Yuiop12345"),
            CreatedById = ObjectId.Empty,
            CreatedDateUtc = DateTime.UtcNow
        };
        await usersCollection.InsertOneAsync(testUser);

        var updateTestUser = new User
        {
            Id = ObjectId.Parse("652c3b89ae02a3135d6309fc"),
            Email = "update@gmail.com",
            Phone = "+380123446789",
            Roles = new List<Role> { userRole },
            PasswordHash = passwordHasher.Hash("Yuiop12345"),
            CreatedById = ObjectId.Empty,
            CreatedDateUtc = DateTime.UtcNow
        };
        await usersCollection.InsertOneAsync(updateTestUser);

        var groupOwner = new User
        {
            Id = ObjectId.Parse("652c3b89ae02a3135d6419fc"),
            Email = "owner@gmail.com",
            Phone = "+380123456689",
            Roles = new List<Role> { userRole, ownerRole },
            PasswordHash = passwordHasher.Hash("Yuiop12345"),
            CreatedById = ObjectId.Empty,
            CreatedDateUtc = DateTime.UtcNow
        };
        await usersCollection.InsertOneAsync(groupOwner);

        var groupUser = new User
        {
            Id = ObjectId.Parse("652c3b89ae02a3135d6439fc"),
            Email = "group@gmail.com",
            Phone = "+380123456889",
            Roles = new List<Role> { userRole },
            PasswordHash = passwordHasher.Hash("Yuiop12345"),
            CreatedById = ObjectId.Empty,
            CreatedDateUtc = DateTime.UtcNow
        };
        await usersCollection.InsertOneAsync(groupUser);

        var groupUser2 = new User
        {
            Id = ObjectId.Parse("652c3b89ae02a3135d6432fc"),
            Email = "group2@gmail.com",
            Phone = "+380123456779",
            Roles = new List<Role> { userRole },
            PasswordHash = passwordHasher.Hash("Yuiop12345"),
            CreatedById = ObjectId.Empty,
            CreatedDateUtc = DateTime.UtcNow
        };
        await usersCollection.InsertOneAsync(groupUser2);

        var adminUser = new User
        {
            Id = ObjectId.Parse("652c3b89ae02a3135d6408fc"),
            Email = "admin@gmail.com",
            Phone = "+12345678901",
            Roles = new List<Role> { userRole, adminRole },
            PasswordHash = passwordHasher.Hash("Yuiop12345"),
            CreatedById = ObjectId.Empty,
            CreatedDateUtc = DateTime.UtcNow
        };
        await usersCollection.InsertOneAsync(adminUser);

        #endregion

        #region RefreshTokens

        var refreshTokensCollection = _dbContext.Db.GetCollection<RefreshToken>("RefreshTokens");

        var refreshToken = new RefreshToken
        {
            Token = "test-refresh-token",
            ExpiryDateUTC = DateTime.UtcNow.AddDays(-7),
            CreatedById = testUser.Id,
            CreatedDateUtc = DateTime.UtcNow
        };
        await refreshTokensCollection.InsertOneAsync(refreshToken);

        #endregion
    }
}