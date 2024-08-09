using Application.Features.Locations.EnterPlaceName;
using Application.Interfaces;

namespace Bot.Services;

public class KeyboardMarkupGenerator : IKeyboardMarkupGenerator
{
    public IAppReplyMarkup BuildKeyboard(string[] items)
    {
        return new KeyboardMarkup(items);
    }
}