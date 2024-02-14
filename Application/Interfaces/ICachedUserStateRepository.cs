namespace Application.Interfaces;

public interface ICachedUserStateRepository
{
    string? GetCache(long userId);
    void SetCache(long userId, string state);
    void RemoveCache(long userId);
}
