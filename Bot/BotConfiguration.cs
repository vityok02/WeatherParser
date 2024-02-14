namespace Bot;

public record BotConfiguration
{
    public static readonly string Configuration = "BotConfiguration";
    public string BotToken { get; init; } = "";
}