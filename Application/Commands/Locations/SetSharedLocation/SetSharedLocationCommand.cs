using Application.Common.Abstract;
using Domain.Locations;

namespace Application.Commands.Locations.SetSharedLocation;

public record SetSharedLocationCommand(long UserId, Coordinates Coordinates) : ICommand;
