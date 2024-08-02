namespace Infrastructure.Weathers;

public sealed class WeatherApiConfiguration
{
    public const string Configuration = "WeatherApi";
    public string Key { get; init; } = string.Empty;
}
