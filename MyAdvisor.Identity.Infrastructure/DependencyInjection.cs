using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyAdvisor.Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureServices(services, configuration);
        ConfigureDatabase(services, configuration);

        return services;
    }

    private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException("Connection string 'UserIdentityDbContext' not found.");
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddDbContext<UserIdentityDbContext>((sp, options) => options
            .UseNpgsql(connectionString, b => b.EnableRetryOnFailure())
            .UseSnakeCaseNamingConvention()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .AddInterceptors(sp.GetRequiredService<IDbCommandInterceptor>()));
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
      
    }
}