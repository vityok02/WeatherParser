using Application.Features.Weathers;
using Application.Interfaces;
using Domain.Users;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.Users;
using Infrastructure.Geocoding;
using Infrastructure.Weathers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = GetConnectionString(configuration);

        connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddHttpClient<IWeatherApiService, WeatherApiService>((sp, httpClient) =>
        {
            httpClient.BaseAddress = new Uri("https://api.weatherapi.com");
        })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new SocketsHttpHandler
                {
                    PooledConnectionLifetime = TimeSpan.FromMinutes(2)
                };
            });

        services.Configure<GeocodingConfiguration>(
            configuration.GetSection(GeocodingConfiguration.Configuration));
        services.Configure<WeatherApiConfiguration>(
            configuration.GetSection(WeatherApiConfiguration.Configuration));

        services.AddScoped<UserRepository>();
        services.AddScoped<IUserRepository, CachedUserRepository>();
        services.AddScoped<ICachedUserStateRepository, CachedUserStateRepository>();
        services.AddScoped<IGeocodingService, GeocodingService>();
        services.AddScoped<ICachedPlacesRepository, CachedPlacesRepository>();

        services.AddMemoryCache();

        return services;
    }

    private static string? GetConnectionString(IConfiguration configuration)
    {
        var connectionString = string.Empty;
        bool useDocker = false;

        var v = Environment.GetEnvironmentVariable("UseDocker");

        _ = bool.TryParse(Environment.GetEnvironmentVariable("UseDocker"), out useDocker);

        if (useDocker)
        {
            connectionString = configuration.GetConnectionString("docker-compose");
        }
        else
        {
            connectionString = configuration.GetConnectionString("docker");
        }

        return connectionString;
    }
}
