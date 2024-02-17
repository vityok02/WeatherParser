using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Features.Locations.EnterPlaceName;

public static class ReplyMarkupHelper
{
    public static ReplyKeyboardMarkup GetReplyKeyboardMarkup(string[] elements)
    {
        var keyboardButtons = GetKeyboardButtons(elements);
        return new ReplyKeyboardMarkup(keyboardButtons);
    }

    public static KeyboardButton[][] GetKeyboardButtons(string[] elements)
    {
        var keyboardButtons = new KeyboardButton[elements.Length][];

        for (int i = 0; i < elements.Length; i++)
        {
            keyboardButtons[i] = [elements[i]];
        }
        return keyboardButtons;
    }
}