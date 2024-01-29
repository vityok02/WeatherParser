using WeatherParser.Models.Interfaces;

namespace WeatherParser.Services.WeatherServices;

public class TimerService : ITimerService
{
    public TimeOnly GetTimeNow() => TimeOnly.FromDateTime(DateTime.Now);

    public bool TimeIsReady(TimeOnly runAt) =>
        runAt.Hour == GetTimeNow().Hour &&
        runAt.Minute == GetTimeNow().Minute;
}