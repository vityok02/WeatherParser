using Application.Abstract;
using Application.Behaviors;
using Application.Interfaces;
using Application.Services;
using Application.Services.Commands;
using Application.Services.Commands.Strategy;
using Application.Services.HtmlProcessing;
using CoreHtmlToImage;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = AssemblyReference.Assembly;

        services.AddMediatR(configuration =>
            configuration
                .RegisterServicesFromAssembly(assembly));

        services
            .AddScoped<TableConverter>()
            .AddScoped<IFile, FileWrapper>()
            .AddScoped<ForecastTableGenerator>()
            .AddScoped<HtmlBuilder>()
            .AddScoped<HtmlToImageConverter>()
            .AddScoped<HtmlTableBuilder>()
            .AddScoped<HtmlConverter>()
            .AddScoped<ICommandProcessor, CommandProcessor>()
            .AddScoped<CommandFactory>()
            .AddScoped<ICommandFactory, CommandFactory>()
            .AddScoped<IUserService, UserService>()
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBegavior<,>))
            .AddStrategies()
            ;

        return services;
    }

    private static IServiceCollection AddStrategies(this IServiceCollection services)
    {
        return services
            .AddScoped<ICommandStrategy, SetSharedLocationStrategy>()
            .AddScoped<ICommandStrategy, LocationRequestStrategy>()
            .AddScoped<ICommandStrategy, UserStateStrategy>()
            .AddScoped<ICommandStrategy, BotCommandStrategy>();
    }
}
