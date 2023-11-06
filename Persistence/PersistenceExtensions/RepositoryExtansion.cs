using Microsoft.Extensions.DependencyInjection;
using Persistence.Database;
using SportComplexResourceOptimization.Persistence.Repositories;
using SportComplexResourceOptimizationApi.Application.IRepository;

namespace SportComplexResourceOptimizationApi.Persistence.PersistenceExtensions;

public static class RepositoryExtansion
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<MongoDbContext>();

        services.AddScoped<IUsersRepository, UsersRepository>();

        return services;
    }
}


