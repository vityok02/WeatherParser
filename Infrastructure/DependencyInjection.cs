using Application.Interfaces;
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
        var connectionString = configuration.GetConnectionString("localDb");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<UserRepository>();
        services.AddScoped<IUserRepository, CachedUserRepository>();
        services.AddScoped<IUserStateRepository, UserStateRepository>();
        services.AddScoped<IGeocodingService, GeocodingService>();
        services.AddScoped<IPlacesRepository, CachedPlacesRepository>();

        services.AddMemoryCache();

        return services;
    }
}
