using Application.Common.Abstract;
using Domain.Locations;

namespace Application.Commands.Weathers.Commands.SendWeatherNow;

public sealed record SendWeatherNowCommand(long UserId, Coordinates Coordinates) : ICommand;
