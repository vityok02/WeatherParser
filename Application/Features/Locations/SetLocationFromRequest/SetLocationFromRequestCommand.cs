using Application.Abstract;
using Domain.Locations;

namespace Application.Locations.SetLocationFromRequest;

public record SetLocationFromRequestCommand(long UserId, Coordinates Coordinates) : ICommand;
