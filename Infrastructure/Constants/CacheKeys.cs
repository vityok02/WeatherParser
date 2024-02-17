namespace Infrastructure.Constants;

public static class CacheKeys
{
    public static Func<long, string> UserById = userId => $"user-{userId}";
    public static Func<long, string> UserStateByUserId = userId => $"user-state-{userId}";
    public static Func<long, string> PlacesByUserId = userId => $"places-{userId}";
}
