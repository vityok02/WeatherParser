namespace Infrastructure.Constants;

public static class CacheKeys
{
    public static Func<long, string> UserById = userId => $"user-{userId}";
}
