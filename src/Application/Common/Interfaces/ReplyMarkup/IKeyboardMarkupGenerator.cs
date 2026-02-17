using Application.Common.Abstract;

namespace Application.Common.Interfaces.ReplyMarkup;

public interface IKeyboardMarkupGenerator
{
    IAppReplyMarkup BuildKeyboard(string[] items);
    IAppReplyMarkup BuildKeyboard(IEnumerable<IEnumerable<string>> items);
}
