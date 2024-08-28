namespace Application.Common.Interfaces;

public interface ITranslationService
{
    Task SetLanguageByUserId(long userId, CancellationToken cancellationToken);
    string StartMessage { get; }
    string ChangeLocationButton { get; }
    string CurrentForecastButton { get; }
    string ForecastTodayButton { get; }
    string ForecastTomorrowButton { get; }
    string RequestLanguage { get; }
}