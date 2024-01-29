namespace WeatherParser.Models.Interfaces;

public interface ITimerService
{
    TimeOnly GetTimeNow();
    bool TimeIsReady(TimeOnly runAt);
}
