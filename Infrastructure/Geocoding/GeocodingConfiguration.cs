namespace Infrastructure;

public record GeocodingConfiguration
{
    public static readonly string Configuration = "GeocodingConfiguration";
    public string Path { get; init; } = "";
    public string Token { get; init; } = "";
}
