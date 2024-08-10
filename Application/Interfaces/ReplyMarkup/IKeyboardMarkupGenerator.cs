using Application.Abstract;

namespace Application.Interfaces.ReplyMarkup;

public interface IKeyboardMarkupGenerator
{
    IAppReplyMarkup BuildKeyboard(string[] elements);
}
