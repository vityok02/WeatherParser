using Application.Common.Abstract;

namespace Application.Commands.Locations.SetLocation;

public record SetLocationCommand(long UserId, string PlaceName) : ICommand;
