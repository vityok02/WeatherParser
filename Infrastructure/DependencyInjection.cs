using Application.Features.Weathers;
using Application.Interfaces;
using Domain.Users;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.Users;
using Infrastructure.Services;
using Infrastructure.Services.Geocoding;
using Infrastructure.Services.WeatherApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);

        services.AddConfigurations(configuration);

        services.AddClients();

        services.AddScoped<UserRepository>()
            .AddScoped<IUserRepository, CachedUserRepository>()
            .AddScoped<IUserStateRepository, UserStateRepository>()
            .AddScoped<IGeocodingService, GeocodingService>()
            .AddScoped<IPlacesRepository, PlacesRepository>()
            .AddScoped<IStyleLoader, StyleLoader>()
            .AddScoped<IWeatherApiUriBuilder, WeatherApiUriBuilder>()
            ;

        services.AddMemoryCache();

        return services;
    }

    private static IServiceCollection AddDbContext(
        this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = GetConnectionString(configuration);

        connectionString = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString,
                providerOptions => providerOptions.EnableRetryOnFailure(10));
        });

        return services;
    }

    private static IServiceCollection AddConfigurations(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<GeocodingConfiguration>(
            configuration.GetSection(GeocodingConfiguration.Configuration));
        services.Configure<WeatherApiConfiguration>(
            configuration.GetSection(WeatherApiConfiguration.Configuration));

        return services;
    }

    private static IServiceCollection AddClients(
        this IServiceCollection services)
    {
        services.AddHttpClient<IWeatherApiService, WeatherApiService>((sp, httpClient) =>
        {
            var settings = sp.GetRequiredService<IOptions<WeatherApiConfiguration>>().Value;
            httpClient.BaseAddress = new Uri(settings.BaseUrl);
        })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new SocketsHttpHandler
                {
                    PooledConnectionLifetime = TimeSpan.FromMinutes(2)
                };
            });

        services.AddHttpClient<IGeocodingService, GeocodingService>((sp, httpClient) =>
        {
            var settings = sp.GetRequiredService<IOptions<GeocodingConfiguration>>().Value;
            httpClient.BaseAddress = new Uri(settings.Path);
        })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new SocketsHttpHandler
                {
                    PooledConnectionLifetime = TimeSpan.FromMinutes(2)
                };
            });

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
