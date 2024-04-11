namespace Infrastructure.Weathers.WeatherApi;

public record WeatherApiConfiguration
{
    public static readonly string Configuration = "WeatherApiConfiguration";
    public string Path { get; init; } = "";
    public string Token { get; init; } = "";
}
