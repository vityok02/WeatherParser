namespace Infrastructure.Constants;

public static class CacheKeys
{
    public static Func<long, string> UserById =
        userId => $"user-{userId}";
    public static Func<long, string> UserStateByUserId =
        userId => $"user-state-{userId}";
    public static Func<long, string> PlacesByUserId =
        userId => $"places-{userId}";
    public static Func<long, string> UserLanguageById =
        userId => $"languages-{userId}";
    public static Func<long, string> UserSessionById =
        userId => $"user-session-{userId}";
}
