using Application.Abstract;
using Application.Interfaces;
using Application.Services;
using Application.Services.HtmlProcessing;
using CoreHtmlToImage;
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
            ;

        return services;
    }
}
