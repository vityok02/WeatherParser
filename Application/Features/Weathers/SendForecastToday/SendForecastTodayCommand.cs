using Application.Abstract;
using Domain.Locations;

namespace Application.Features.Weathers.SendForecastToday;

public sealed record SendForecastTodayCommand(long ChatId, Coordinates Coordinates) : ICommand;
