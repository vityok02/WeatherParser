using Application.Common.Abstract;
using Application.Common.Interfaces.ReplyMarkup;
using Bot.TgTypes;

namespace Bot.Services;

public class KeyboardMarkupGenerator : IKeyboardMarkupGenerator
{
    public IAppReplyMarkup BuildKeyboard(string[] items)
    {
        return new AppKeyboardMarkup(items);
    }

    public IAppReplyMarkup BuildKeyboard(IEnumerable<IEnumerable<string>> items)
    {
        return new AppKeyboardMarkup(items);
    }
}