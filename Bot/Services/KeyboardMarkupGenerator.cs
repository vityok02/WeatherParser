using Application.Interfaces.ReplyMarkup;

namespace Bot.Services;

public class KeyboardMarkupGenerator : IKeyboardMarkupGenerator
{
    public IAppReplyMarkup BuildKeyboard(string[] items)
    {
        return new KeyboardMarkup(items);
    }
}