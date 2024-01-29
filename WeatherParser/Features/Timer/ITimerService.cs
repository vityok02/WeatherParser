namespace WeatherParser.Features.Timer;

public interface ITimerService
{
    TimeOnly GetTimeNow();
    bool TimeIsReady(TimeOnly runAt);
}
