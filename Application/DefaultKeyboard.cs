using Application.Common.Abstract;
using Application.Common.Interfaces.ReplyMarkup;
using Common.Constants;

namespace Application;
public static class DefaultKeyboard
{
    public static IAppReplyMarkup GetKeyboard(IKeyboardMarkupGenerator keyboardGenerator)
    {
        IEnumerable<IEnumerable<string>> buttons = [
            [BotCommand.WeatherNow, BotCommand.ForecastToday, BotCommand.ForecastTomorrow], 
            [BotCommand.ChangeLocation, BotCommand.ChangeLanguage]
        ];

        var keyboard = keyboardGenerator.BuildKeyboard(buttons);
        return keyboard;
    }
}
