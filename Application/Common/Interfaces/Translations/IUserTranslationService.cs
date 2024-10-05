using Application.Common.Interfaces.Localization;
using Domain.Languages;
using Domain.Translations;
using Domain.Users;

namespace Application.Common.Interfaces.Translations;

public interface IUserTranslationService
{
    Task<Language> GetUserLanguage(long userId, CancellationToken token);
    Task<Translation> GetUserTranslationAsync(long userId, CancellationToken cancellationToken);
}

public class UserTranslationService : IUserTranslationService
{
    private readonly ITranslationService _translationService;
    private readonly IUserRepository _userRepository;

    public UserTranslationService(
        ITranslationService translationService,
        IUserRepository userRepository)
    {
        _translationService = translationService;
        _userRepository = userRepository;
    }

    public async Task<Translation> GetUserTranslationAsync(long userId, CancellationToken token)
    {
        var language = await _userRepository.GetLanguageAsync(userId, token);

        return _translationService.GetTranslation(language?.Name ?? Languages.English);
    }

    public async Task<Language> GetUserLanguage(long userId, CancellationToken token)
    {
        var language = await _userRepository.GetLanguageAsync(userId, token);

        return language!;
    }
}