using Application.Common.Interfaces;
using Common.Constants;
using Domain;
using Domain.Users;
using Infrastructure.Translations.Interfaces;

namespace Infrastructure.Translations;

public class TranslationService : ITranslationService
{
    private readonly ITextProvider _textProvider;
    private string _language = Languages.English;
    private readonly IUserRepository _userRepository;

    public TranslationService(ITextProvider textProvider, IUserRepository userRepository)
    {
        _textProvider = textProvider;
        _userRepository = userRepository;
    }

    public async Task SetLanguageByUserId(long userId, CancellationToken cancellationToken)
    {
        var language = await _userRepository.GetLanguageAsync(userId, cancellationToken);
        _language = language?.Name ?? Languages.English;
    }

    // TODO: replace languages to TextProvider

    public string StartMessage =>
        _textProvider.GetMessageTranslation(nameof(StartMessage), _language);

    public string CurrentForecastButton =>
        _textProvider.GetButtonTranslation(nameof(CurrentForecastButton), _language);

    public string ForecastTodayButton =>
        _textProvider.GetButtonTranslation(nameof(ForecastTodayButton), _language);

    public string ForecastTomorrowButton =>
        _textProvider.GetButtonTranslation(nameof(ForecastTomorrowButton), _language);

    public string ChangeLocationButton =>
        _textProvider.GetButtonTranslation(nameof(ChangeLocationButton), _language);

    public string RequestLanguage =>
        _textProvider.GetMessageTranslation(nameof(RequestLanguage), _language);
}
