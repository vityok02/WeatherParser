namespace Application.Interfaces;

public interface IUserStateRepository
{
    string? GetState(long userId);
    void SetState(long userId, string state);
    void RemoveState(long userId);
}
