namespace Infrastructure.Weathers;

public record WeatherClientConfiguration
{
    public static readonly string Configuration = "WeatherConfiguration";
    public string Path { get; init; } = "";
    public string Parametes { get; init; } = "";
}
