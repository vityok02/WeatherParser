namespace Bot;

public record BotConfiguration
{
    public const string Configuration = "BotConfiguration";
    public string BotToken { get; init; } = "";
}