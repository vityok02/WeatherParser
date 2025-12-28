using Application.Common.Abstract;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.TgTypes;

public class AppKeyboardMarkup : IAppReplyMarkup
{
    public ReplyKeyboardMarkup TelegramReplyKeyboardMarkup { get; }

    public AppKeyboardMarkup(string[] buttons)
    {
        var keyboardButtons = GetKeyboardButtons(buttons);
        TelegramReplyKeyboardMarkup = new ReplyKeyboardMarkup(keyboardButtons);
    }

    public AppKeyboardMarkup(IEnumerable<IEnumerable<string>> buttons)
    {
        TelegramReplyKeyboardMarkup = GetKeyboard(buttons);
    }

    private static KeyboardButton[][] GetKeyboardButtons(string[] elements)
    {
        var keyboardButtons = new KeyboardButton[elements.Length][];

        for (int i = 0; i < elements.Length; i++)
        {
            keyboardButtons[i] = [new KeyboardButton(elements[i])];
        }
        return keyboardButtons;
    }

    private static ReplyKeyboardMarkup GetKeyboard(IEnumerable<IEnumerable<string>> buttons)
    {
        IEnumerable<IEnumerable<KeyboardButton>> keyboard = buttons
            .Select(b => b.Select(x => new KeyboardButton(x)));
        return new ReplyKeyboardMarkup(keyboard);
    }
}