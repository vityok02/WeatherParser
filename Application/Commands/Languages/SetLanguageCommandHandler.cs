using Application.Commands.Default;
using Application.Common.Abstract;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Localization;
using Application.Common.Interfaces.Messaging;
using Domain.Abstract;
using Domain.Languages;
using Domain.Users;

namespace Application.Commands.Languages;

internal class SetLanguageCommandHandler
    : ICommandHandler<SetLanguageCommand>
{
    private readonly IMessageSender _messageSender;
    private readonly IUserRepository _userRepository;
    private readonly ILanguageRepository _languageRepository;
    private readonly ISessionManager _sessionManager;
    private readonly ITranslationService _translationService;
    private readonly IDefaultKeyboardFactory _defaultKeyboardFactory;

    public SetLanguageCommandHandler(
        IUserRepository userRepository,
        ILanguageRepository languageRepository,
        IMessageSender messageSender,
        ITranslationService translationService,
        IDefaultKeyboardFactory defaultKeyboardFactory,
        ISessionManager sessionManager)
    {
        _userRepository = userRepository;
        _languageRepository = languageRepository;
        _messageSender = messageSender;
        _translationService = translationService;
        _defaultKeyboardFactory = defaultKeyboardFactory;
        _sessionManager = sessionManager;
    }

    public async Task<Result> Handle(
        SetLanguageCommand command,
        CancellationToken cancellationToken)
    {
        var language = await _languageRepository
            .GetByName(command.Language, cancellationToken);

        var translation = _translationService
            .GetTranslation(language.Name);

        if (language is null)
        {
            await _messageSender.SendTextMessageAsync(
                command.UserId,
                translation.Messages["LanguageFail"],
                cancellationToken);

            return Result.Failure("Language is null");
        }

        var user = await _userRepository
            .GetByIdAsync(command.UserId, cancellationToken);
        user!.UpdateLanguage(language);

        await _userRepository
            .SaveChangesAsync(cancellationToken);

        await _messageSender.SendTextMessageAsync(
            command.UserId,
            translation.Messages["LanguageSet"],
            _defaultKeyboardFactory
                .CreateKeyboard(translation),
            cancellationToken);

        var userSession = _sessionManager
            .GetOrCreateSession(command.UserId);

        userSession.Remove("state");

        return Result.Success();
    }
}
