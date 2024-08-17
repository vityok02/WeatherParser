using Application.Common.Abstract;
using Application.Common.Behaviors;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Application.Services;
using Application.Services.Bot.Commands;
using Application.Services.Bot.Strategies;
using Application.Services.HtmlProcessing;
using Application.Users;
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
            .AddScoped<IHtmlTableBuilder, HtmlTableBuilder>()
            .AddScoped<HtmlConverter>()
            .AddScoped<ICommandProcessor, CommandProcessor>()
            .AddScoped<CommandFactory>()
            .AddScoped<ICommandFactory, CommandFactory>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IUserSession, UserSession>()
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
