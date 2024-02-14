namespace Domain.Users;

public static class UserExtensions
{
    public static User ToAppUser(this Telegram.Bot.Types.User user)
    {
        return new User(user.Id);
    }
}
