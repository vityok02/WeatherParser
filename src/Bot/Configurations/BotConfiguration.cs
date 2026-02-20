namespace Bot.Configurations;

public record BotConfiguration
{
    public const string Configuration = "BotConfiguration";

    public string HostAddress { get; init; } = string.Empty;

    public string BotToken { get; init; } = string.Empty;
}
