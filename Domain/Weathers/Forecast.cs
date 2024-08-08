namespace Domain.Weathers;

public class Forecast
{
    public IEnumerable<DailyForecast> DailyForecast { get; private set; }

    public Forecast(IEnumerable<DailyForecast> dailyForecast)
    {
        DailyForecast = dailyForecast;
    }
}
