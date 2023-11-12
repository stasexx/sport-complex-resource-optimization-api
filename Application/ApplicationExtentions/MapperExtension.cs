using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SportComplexResourceOptimizationApi.Application.MappingProfile;

namespace SportComplexResourceOptimizationApi.Application.ApplicationExtentions;

public static class MapperExtension
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(UserProfile)));
        services.AddAutoMapper(Assembly.GetAssembly(typeof(RoleProfile)));
        services.AddAutoMapper(Assembly.GetAssembly(typeof(ServiceProfile)));
        services.AddAutoMapper(Assembly.GetAssembly(typeof(SportComplexProfile)));
        services.AddAutoMapper(Assembly.GetAssembly(typeof(AbonnementProfile)));

        return services;
    }
}