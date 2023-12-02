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
        InitializeSportComplexesAsync().Wait();
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
            Id = ObjectId.Parse("652c3b89ae02a3135d5409fc"),
            Email = "someone@gmail.com",
            Roles = new List<Role> { userRole },
            PasswordHash = passwordHasher.Hash("qwerty1234"),
            CreatedById = ObjectId.Empty,
            CreatedDateUtc = DateTime.UtcNow
        };
        await usersCollection.InsertOneAsync(testUser);

        var updateTestUser = new User
        {
            Id = ObjectId.Parse("652c3b89ae02a3135d6309fc"),
            Email = "someoneForUpdate@gmail.com",
            Phone = "+380123446789",
            Roles = new List<Role> { userRole },
            PasswordHash = passwordHasher.Hash("update12345"),
            CreatedById = ObjectId.Empty,
            CreatedDateUtc = DateTime.UtcNow
        };
        await usersCollection.InsertOneAsync(updateTestUser);

        var groupOwner = new User
        {
            Id = ObjectId.Parse("652c3b89ae02a3135d6519fc"),
            Email = "ownerAdrenaline@gmail.com",
            Phone = "+380123456689",
            Roles = new List<Role> { ownerRole },
            PasswordHash = passwordHasher.Hash("adrenaline1234"),
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

    public async Task InitializeSportComplexesAsync()
    {
        var sportComplexCollection = _dbContext.Db.GetCollection<SportComplex>("SportComplexes");
        var testSportComplex1 = new SportComplex
    {
        Id = ObjectId.Parse("652c3b89ae02a3135d5409fd"),
        Name = "Sport Complex 1",
        Email = "sportcomplex1@example.com",
        City = "Zaporisha",
        Address = "123 Main St, City",
        OperatingHours = "9:00 AM - 5:00 PM",
        Description = "A description for Sport Complex 1",
        Rating = 4.5,
        CreatedById = ObjectId.Parse("652c3b89ae02a3135d6519fc"),
        CreatedDateUtc = DateTime.UtcNow
    };

    var testSportComplex2 = new SportComplex
    {
        Id = ObjectId.Parse("652c3b89ae02a3135d5409fe"),
        Name = "Sport Complex 2",
        Email = "sportcomplex2@example.com",
        City = "Zaporisha",
        Address = "456 Oak St, Town",
        OperatingHours = "10:00 AM - 6:00 PM",
        Description = "A description for Sport Complex 2",
        Rating = 3.8,
        CreatedById = ObjectId.Parse("652c3b89ae02a3135d6519fc"),
        CreatedDateUtc = DateTime.UtcNow
    };

    var testSportComplex3 = new SportComplex
    {
        Id = ObjectId.Parse("652c3b89ae02a3135d5409ff"),
        Name = "Sport Complex 3",
        Email = "sportcomplex3@example.com",
        Address = "789 Pine St, Village",
        OperatingHours = "8:00 AM - 4:00 PM",
        Description = "A description for Sport Complex 3",
        Rating = 4.0,
        City = "Zaporisha",
        CreatedById = ObjectId.Parse("652c3b89ae02a3135d6519fc"),
        CreatedDateUtc = DateTime.UtcNow
    };

    var testSportComplex4 = new SportComplex
    {
        Id = ObjectId.Parse("652c3b89ae02a3135d540a00"),
        Name = "Sport Complex 4",
        Email = "sportcomplex4@example.com",
        City = "Dnipro",
        Address = "101 Elm St, Suburb",
        OperatingHours = "11:00 AM - 7:00 PM",
        Description = "A description for Sport Complex 4",
        Rating = 4.2,
        CreatedById = ObjectId.Empty,
        CreatedDateUtc = DateTime.UtcNow
    };

    var testSportComplex5 = new SportComplex
    {
        Id = ObjectId.Parse("652c3b89ae02a3135d540a01"),
        Name = "Sport Complex 5",
        Email = "sportcomplex5@example.com",
        City = "Dnipro",
        Address = "202 Maple St, Countryside",
        OperatingHours = "10:00 AM - 6:00 PM",
        Description = "A description for Sport Complex 5",
        Rating = 3.7,
        CreatedById = ObjectId.Empty,
        CreatedDateUtc = DateTime.UtcNow
    };
    
    await sportComplexCollection.InsertOneAsync(testSportComplex1);
    await sportComplexCollection.InsertOneAsync(testSportComplex2);
    await sportComplexCollection.InsertOneAsync(testSportComplex3);
    await sportComplexCollection.InsertOneAsync(testSportComplex4);
    await sportComplexCollection.InsertOneAsync(testSportComplex5);
    }
}