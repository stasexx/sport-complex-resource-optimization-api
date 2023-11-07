using Microsoft.Extensions.DependencyInjection;
using Persistence.Database;
using SportComplexResourceOptimizationApi.Persistence.Repositories;
using SportComplexResourceOptimizationApi.Application.IRepositories;

namespace SportComplexResourceOptimizationApi.Persistence.PersistenceExtensions;

public static class RepositoryExtansion
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<MongoDbContext>();

        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IRolesRepository, RolesRepository>();
        services.AddScoped<IRefreshTokensRepository, RefreshTokensRepository>();
        services.AddScoped<ISportComplexesRepository, SportComplexesRepository>();

        return services;
    }
}


