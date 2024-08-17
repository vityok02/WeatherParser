using Common.Constants;

namespace Application.Common.Interfaces.Repositories;

public interface IUserStateRepository
{
    UserState? GetState(long userId);
    void SetState(long userId, UserState state);
    void RemoveState(long userId);
}
