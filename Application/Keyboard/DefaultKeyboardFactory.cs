using Application.Commands.Default;
using Application.Common.Abstract;
using Application.Common.Constants;
using Application.Common.Interfaces.ReplyMarkup;
using Domain.Translations;

namespace Application.Keyboard;
public class DefaultKeyboardFactory : IDefaultKeyboardFactory
{
    private readonly IKeyboardMarkupGenerator _keyboardGenerator;

    public DefaultKeyboardFactory(IKeyboardMarkupGenerator keyboardGenerator)
    {
        _keyboardGenerator = keyboardGenerator;
    }

    public IAppReplyMarkup CreateKeyboard(
        Translation translation)
    {
        IEnumerable<IEnumerable<string>> buttons = [
            [
                translation.Buttons[Buttons.CurrentWeather],
                translation.Buttons[Buttons.ForecastToday],
                translation.Buttons[Buttons.ForecastTomorrow]
            ],
            [
                translation.Buttons[Buttons.ViewLocation],
                translation.Buttons[Buttons.ChangeLocation],
                translation.Buttons[Buttons.ChangeLanguage],
            ]
        ];

        var keyboard = _keyboardGenerator.BuildKeyboard(buttons);
        return keyboard;
    }
}
