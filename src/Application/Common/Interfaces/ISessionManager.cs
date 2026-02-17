using Application.Users;

namespace Application.Common.Interfaces;

public interface ISessionManager
{
    UserSession GetOrCreateSession(long userId);
    void RemoveSession(long userId);
}