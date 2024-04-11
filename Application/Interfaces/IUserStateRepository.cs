namespace Application.Interfaces;

public interface IUserStateRepository
{
    string? GetUserState(long userId);
    void SetUserState(long userId, string state);
    void RemoveUserState(long userId);
    void UpdateUserState(long userId, string state);
}
