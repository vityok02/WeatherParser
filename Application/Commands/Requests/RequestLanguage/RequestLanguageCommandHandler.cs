using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.ReplyMarkup;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Translations;
using Common.Constants;
using Domain.Abstract;
using Domain.Languages;

namespace Application.Commands.Requests.RequestLanguage;

internal class RequestLanguageCommandHandler : ICommandHandler<RequestLanguageCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly IUserTranslationService _translationService;
    private readonly IKeyboardMarkupGenerator _keyboardMarkupGenerator;
    private readonly ILanguageRepository _languageRepository;
    private readonly IUserStateRepository _userStateRepository;
    private readonly ISessionManager _sessionManager;

    public RequestLanguageCommandHandler(
        IMessageSender messageSender,
        IUserTranslationService translationService,
        IKeyboardMarkupGenerator keyboardMarkupGenerator,
        ILanguageRepository languageRepository,
        IUserStateRepository userStateRepository,
        ISessionManager sessionManager)
    {
        _messageSender = messageSender;
        _translationService = translationService;
        _keyboardMarkupGenerator = keyboardMarkupGenerator;
        _languageRepository = languageRepository;
        _userStateRepository = userStateRepository;
        _sessionManager = sessionManager;
    }

    public async Task<Result> Handle(
        RequestLanguageCommand command,
        CancellationToken cancellationToken)
    {
        var translation = await _translationService
            .GetUserTranslationAsync(command.UserId, cancellationToken);

        List<string> buttons = [];

        var languages = await _languageRepository
            .GetAll(cancellationToken);

        foreach (var l in languages.ToList())
        {
            buttons.Add(l.Name);
        }

        var keyboard = _keyboardMarkupGenerator
            .BuildKeyboard(buttons.ToArray());

        await _messageSender.SendTextMessageAsync(
            chatId: command.UserId,
            text: translation.Messages["RequestLanguage"],
            replyMarkup: keyboard,
            cancellationToken: cancellationToken);

        var userSession = _sessionManager.GetOrCreateSession(command.UserId);

        userSession.Set("state", UserState.ChangeLanguage);

        return Result.Success();
    }
}
