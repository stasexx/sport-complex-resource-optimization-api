
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using Persistence.Database;
using SportComplexResourceOptimizationApi.Entities;

namespace SportComplexResourceOptimizationApi.Persistence.PersistenceExtensions;

public class DbInitialaizer 
{
    private readonly IMongoCollection<User> _userCollection;

    public DbInitialaizer(IServiceProvider serviceProvider)
    {
        _userCollection = serviceProvider.GetService<MongoDbContext>().Db.GetCollection<User>("Users");
    }

    public async Task InitialaizeDb(CancellationToken cancellationToken)
    {
        await AddUsers(cancellationToken);
    }

    public async Task AddUsers(CancellationToken cancellationToken)
    {

        var users = new User[]
        {
            new User
            {
                Id = ObjectId.Parse("6533bb29c8c22b038c71cf46"),
                GuestId = Guid.NewGuid(),
                CreatedById = ObjectId.Parse("6533bb29c8c22b038c71cf46"),
                CreatedDateUtc = DateTime.UtcNow,
                LastModifiedById = ObjectId.Parse("6533bb29c8c22b038c71cf46"),
                LastModifiedDateUtc = DateTime.UtcNow,
                IsDeleted = false
            },

            new User
            {
                Id = ObjectId.Parse("6533bde5755745116be42ce7"),
                GuestId = Guid.NewGuid(),
                Phone = "+380953326869",
                Email = "mykhailo.bilodid@nure.ua",
                PasswordHash = "Yuiop12345",
                CreatedById = ObjectId.Parse("6533bde5755745116be42ce7"),
                CreatedDateUtc = DateTime.UtcNow,
                LastModifiedById = ObjectId.Parse("6533bde5755745116be42ce7"),
                LastModifiedDateUtc = DateTime.UtcNow,
                IsDeleted = false
            },

            new User
            {
                Id = ObjectId.Parse("6533bded80fbc6e96250575b"),
                GuestId = Guid.NewGuid(),
                Phone = "+380953826869",
                Email = "shopping.assistant.team@gmail.com",
                PasswordHash = "Yuiop12345",
                CreatedById = ObjectId.Parse("6533bded80fbc6e96250575b"),
                CreatedDateUtc = DateTime.UtcNow,
                LastModifiedById = ObjectId.Parse("6533bded80fbc6e96250575b"),
                LastModifiedDateUtc = DateTime.UtcNow,
                IsDeleted = false            
                }
        };

        await _userCollection.InsertManyAsync(users);
    }
}