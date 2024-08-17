namespace Application.Users;

public interface IUserSession
{
    T? Get<T>(string key);
    void Set<T>(string key, T value);
}