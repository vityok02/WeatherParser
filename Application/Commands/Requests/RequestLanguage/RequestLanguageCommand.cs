using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.ReplyMarkup;
using Common.Constants;
using Domain.Abstract;
using Domain.Languages;

namespace Application.Commands.Requests.RequestLanguage;

public record RequestLanguageCommand(long UserId) : ICommand;

internal class RequestLanguageCommandHandler : ICommandHandler<RequestLanguageCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly ITranslationService _translationService;
    private readonly IKeyboardMarkupGenerator _keyboardMarkupGenerator;
    private readonly ILanguageRepository _languageRepository;

    public RequestLanguageCommandHandler(
        IMessageSender messageSender,
        ITranslationService translationService,
        IKeyboardMarkupGenerator keyboardMarkupGenerator,
        ILanguageRepository languageRepository)
    {
        _messageSender = messageSender;
        _translationService = translationService;
        _keyboardMarkupGenerator = keyboardMarkupGenerator;
        _languageRepository = languageRepository;
    }

    public async Task<Result> Handle(
        RequestLanguageCommand command,
        CancellationToken cancellationToken)
    {
        IAppReplyMarkup keyboard = DefaultKeyboard.GetKeyboard(_keyboardMarkupGenerator);

        List<string> buttons = [];

        var languages = await _languageRepository.GetAll(cancellationToken);

        foreach (var language in languages.ToList())
        {
            buttons.Add(language.Name);
        }

        await _translationService.SetLanguageByUserId(command.UserId, cancellationToken);

        await _messageSender.SendTextMessageAsync(
            chatId: command.UserId,
            text: _translationService.RequestLanguage,
            replyMarkup: keyboard,
            cancellationToken: cancellationToken);

        return Result.Success();
    }
}
