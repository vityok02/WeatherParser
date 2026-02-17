using Application;
using Bot;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPresentation(builder.Configuration);

builder.Services.AddHealthChecks();

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();

try
{
    await using var scope = app.Services.CreateAsyncScope();
    using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await dbContext.Database.MigrateAsync();
    await DataSeeder.SeedDataAsync(dbContext);

    logger.LogInformation("Database migration & seed completed");
}
catch (Exception ex)
{
    logger.LogError(ex, "Database migration or seed failed");
    throw;
}

app.UseHealthChecks("/health");

await app.RunAsync();
