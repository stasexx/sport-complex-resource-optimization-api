using Microsoft.Extensions.DependencyInjection;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Infrastructure.Services;

namespace SportComplexResourceOptimizationApi.Infrastructure.InfrastructureExtentions;

public static class ServicesExtention
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UsersService>();

        return services;
    }
}