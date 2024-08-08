using Application.Abstract;
using Domain.Locations;

namespace Application.Features.Weathers.SendWeatherNow;

public sealed record SendWeatherNowCommand(long ChatId, Coordinates Coordinates) : ICommand;
