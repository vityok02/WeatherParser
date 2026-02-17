using Application.Common.Abstract;
using Domain.Locations;

namespace Application.Commands.Weathers.Commands.SendMultiDayForecast;

public sealed record SendMultidayForecastCommand(
    long UserId,
    int Days,
    Coordinates Coordinates) 
    : ICommand;
