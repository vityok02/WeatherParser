using Common.Constants;

namespace Application.Interfaces;

public interface IUserStateRepository
{
    UserState? GetState(long userId);
    void SetState(long userId, UserState state);
    void RemoveState(long userId);
}
