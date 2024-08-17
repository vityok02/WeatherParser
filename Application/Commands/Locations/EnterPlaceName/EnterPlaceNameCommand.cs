using Application.Common.Abstract;

namespace Application.Commands.Locations.EnterPlaceName;

public record EnterPlaceNameCommand(long UserId, string PlaceName) : ICommand;
