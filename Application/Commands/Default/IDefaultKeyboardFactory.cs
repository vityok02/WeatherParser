using Application.Common.Abstract;
using Domain.Translations;

namespace Application.Commands.Default;

public interface IDefaultKeyboardFactory
{
    IAppReplyMarkup CreateKeyboard(Translation translation);
}