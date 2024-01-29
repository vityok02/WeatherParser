namespace WeatherParser.Features.UserState;

public interface IUserStateService
{
    string? GetUserState(long userId);
    void RemoveUserState(long userId);
    void ChangeUserState(long userId, string userState);
    void SetUserState(long userId, string state);
}