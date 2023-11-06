using Microsoft.Extensions.DependencyInjection;
using Persistence.Database;
using SportComplexResourceOptimizationApi.Persistence.Repositories;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Application.IRepository;

namespace SportComplexResourceOptimizationApi.Persistence.PersistenceExtensions;

public static class RepositoryExtansion
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<MongoDbContext>();

        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IRolesRepository, RolesRepository>();
        services.AddScoped<IRefreshTokensRepository, RefreshTokensRepository>();

        return services;
    }
}


