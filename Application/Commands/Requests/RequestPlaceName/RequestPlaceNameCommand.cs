using Application.Common.Abstract;

namespace Application.Commands.Requests.RequestPlaceName;

public record RequestPlaceNameCommand(long UserId, string PlaceName) : ICommand;
