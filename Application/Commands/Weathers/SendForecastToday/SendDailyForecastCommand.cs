using Application.Common.Abstract;
using Domain.Locations;

namespace Application.Commands.Weathers.SendForecastToday;

public sealed record SendDailyForecastCommand(
    long ChatId, Coordinates Coordinates, DateTime Date) 
    : ICommand;
