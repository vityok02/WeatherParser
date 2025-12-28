namespace Infrastructure.Constants;

public static class CacheKeys
{
    public static readonly Func<long, string> UserById =
        userId => $"user-{userId}";
    public static readonly Func<long, string> UserStateByUserId =
        userId => $"user-state-{userId}";
    public static readonly Func<long, string> PlacesByUserId =
        userId => $"places-{userId}";
    public static readonly Func<long, string> UserLanguageById =
        userId => $"languages-{userId}";
    public static readonly Func<long, string> UserSessionById =
        userId => $"user-session-{userId}";
}
