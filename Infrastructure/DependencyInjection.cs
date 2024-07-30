﻿using Application.Interfaces;
using Domain.Users;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.Users;
using Infrastructure.Geocoding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = string.Empty;
        bool useDocker = false;

        var v = Environment.GetEnvironmentVariable("UseDocker");

        _ = bool.TryParse(Environment.GetEnvironmentVariable("UseDocker"), out useDocker);

        if (useDocker)
        {
            connectionString = "Server=bot-db,1433;Database=TgBot;User Id=sa;Password=yourStrong(!)Password; TrustServerCertificate=true";
        }
        else
        {
            connectionString = configuration.GetConnectionString("localDb");
        }

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<UserRepository>();
        services.AddScoped<IUserRepository, CachedUserRepository>();
        services.AddScoped<ICachedUserStateRepository, CachedUserStateRepository>();
        services.AddScoped<IGeocodingService, GeocodingService>();
        services.AddScoped<ICachedPlacesRepository, CachedPlacesRepository>();

        services.AddMemoryCache();

        return services;
    }
}
