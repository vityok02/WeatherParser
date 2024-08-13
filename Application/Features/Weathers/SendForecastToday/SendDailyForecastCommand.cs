using Application.Abstract;
using Domain.Locations;

namespace Application.Features.Weathers.SendForecastToday;

public sealed record SendDailyForecastCommand(long ChatId, DateTime Date, Coordinates Coordinates) : ICommand;
