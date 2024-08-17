namespace Application.Users;

public class UserSession : IUserSession
{
    public long UserId { get; }
    public Dictionary<string, object> Data { get; }

    public UserSession(long userId)
    {
        UserId = userId;
        Data = [];
    }

    public T? Get<T>(string key)
    {
        Data.TryGetValue(key, out var value);
        if (value is T tValue)
        {
            return tValue;
        }

        return default!;
    }

    public void Set<T>(string key, T value)
    {
        Data[key] = value!;
    }

    public void Remove(string key)
    {
        Data.Remove(key);
    }
}
