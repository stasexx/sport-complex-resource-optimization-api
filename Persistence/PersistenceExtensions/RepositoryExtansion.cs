using Microsoft.Extensions.DependencyInjection;
using Persistence.Database;
using SportComplexResourceOptimizationApi.Persistence.Repositories;
using SportComplexResourceOptimizationApi.Application.IRepositories;
using SportComplexResourceOptimizationApi.Application.IServices;

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
        services.AddScoped<IServicesRepository, ServicesRepository>();
        services.AddScoped<IEquipmentsRepository, EquipmentsRepository>();
        services.AddScoped<IReservationsRepository, ReservationsRepository>();
        services.AddScoped<IServiceSubscriptionsRepository, ServiceSubscriptionRepository>();
        services.AddScoped<IAbonnementsRepository, AbonnementRepository>();
        services.AddScoped<ISensorsRepository, SensorsRepository>();

        return services;
    }
}


