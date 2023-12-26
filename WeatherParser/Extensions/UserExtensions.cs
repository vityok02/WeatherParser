using WeatherParser.Models;
using TelegramUser = Telegram.Bot.Types.User;

namespace WeatherParser.Extensions;

public static class UserExtensions
{
    public static User ToAppUser(this TelegramUser user)
    {
        return new User()
        {
            Id = user.Id,
            IsBot = user.IsBot,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.Username,
            AddedToAttachmentMenu = user.AddedToAttachmentMenu,
            CanJoinGroups = user.CanJoinGroups,
            CanReadAllGroupMessages = user.CanReadAllGroupMessages,
            IsPremium = user.IsPremium,
            LanguageCode = user.LanguageCode,
            SupportsInlineQueries = user.SupportsInlineQueries,
        };
    }
}
