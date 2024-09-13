using Application.Common.Abstract;
using Domain.Locations;

namespace Application.Commands.Weathers.Commands.SendForecastToday;

public sealed record SendDailyForecastCommand(
    long UserId, Coordinates Coordinates, DateTime Date)
    : ICommand;
