using Microsoft.Extensions.DependencyInjection;
using SportComplexResourceOptimizationApi.Application.IServices;
using SportComplexResourceOptimizationApi.Infrastructure.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SportComplexResourceOptimizationApi.Application.IServices.Identity;
using SmartInventorySystemApi.Infrastructure.Services.Identity;

namespace SportComplexResourceOptimizationApi.Infrastructure.InfrastructureExtentions;

public static class ServicesExtention
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UsersService>();
        services.AddScoped<ITokensService, TokensService>();
        return services;
    }

    public static IServiceCollection AddJWTTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = configuration.GetValue<bool>("JsonWebTokenKeys:ValidateIssuer"),
                    ValidateAudience = configuration.GetValue<bool>("JsonWebTokenKeys:ValidateAudience"),
                    ValidateLifetime = configuration.GetValue<bool>("JsonWebTokenKeys:ValidateLifetime"),
                    ValidateIssuerSigningKey = configuration.GetValue<bool>("JsonWebTokenKeys:ValidateIssuerSigningKey"),
                    ValidIssuer = configuration.GetValue<string>("JsonWebTokenKeys:ValidIssuer"),
                    ValidAudience = configuration.GetValue<string>("JsonWebTokenKeys:ValidAudience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JsonWebTokenKeys:IssuerSigningKey"))),
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}