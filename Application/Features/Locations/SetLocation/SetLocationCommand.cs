using Application.Abstract;
using Domain.Locations;

namespace Application.Features.Locations.SetLocation;

public record SetLocationCommand(long UserId, Coordinates? CoordinatesResponse, string PlaceName) : ICommand;
