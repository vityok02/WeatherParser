using Application.Common.Abstract;
using Domain.Locations;

namespace Application.Locations.SetLocationFromRequest;

public record SetSharedLocationCommand(long UserId, Coordinates Coordinates) : ICommand;
