namespace Domain.Weathers;

public class HourlyForecast
{
    public DateTime Time { get; private set; }
    public double Temp { get; private set; }
    public double FeelsLikeTemp { get; private set; }
    public Condition Condition { get; private set; }
    public double WindSpeed { get; private set; }
    public double Humidity { get; private set; }
    public double Cloud { get; private set; }

    public HourlyForecast(DateTime time, double temp, double feelseLikeTemp, Condition condition, double windSpeed, double humidity, double cloud)
    {
        Time = time;
        Temp = temp;
        FeelsLikeTemp = feelseLikeTemp;
        Condition = condition;
        WindSpeed = windSpeed;
        Humidity = humidity;
        Cloud = cloud;
    }
}
