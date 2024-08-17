using Application.Common.Abstract;
using Domain.Locations;

namespace Application.Commands.Weathers.SendWeatherNow;

public sealed record SendWeatherNowCommand(long ChatId, Coordinates Coordinates) : ICommand;
