namespace Domain.Weathers;

public class DailyForecast
{
    public DateTime Date { get; private set; } = default;
    public double AvgTemp { get; private set; } = default;
    public double MinTemp { get; private set; } = default;
    public double MaxTemp { get; private set; } = default;
    public double MaxWind { get; private set; } = default;
    public double AvgHumidity { get; private set; } = default;
    public Precipitation Precipitation { get; private set; } = default!;
    public Condition Condition { get; private set; } = default!;
    public IEnumerable<HourlyForecast> HourlyForecast { get; private set; } = [];

    public DailyForecast(
        DateTime date,
        double avgTemp,
        double minTemp,
        double maxTemp,
        double maxWind,
        double avgHumidity,
        Precipitation precipitation,
        Condition condition,
        IEnumerable<HourlyForecast> hourlyForecast)
    {
        Date = date;
        AvgTemp = avgTemp;
        MinTemp = minTemp;
        MaxTemp = maxTemp;
        MaxWind = maxWind;
        AvgHumidity = avgHumidity;
        Precipitation = precipitation;
        Condition = condition;
        HourlyForecast = hourlyForecast;
    }
}
