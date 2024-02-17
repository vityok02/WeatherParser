using Application.Abstract;

namespace Application.Features.Locations.EnterPlaceName;

public record EnterPlaceNameCommand(long UserId, string PlaceName) : ICommand;
