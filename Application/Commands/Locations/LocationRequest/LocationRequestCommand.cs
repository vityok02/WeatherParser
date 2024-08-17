using Application.Common.Abstract;

namespace Application.Commands.Locations.LocationRequest;

public sealed record LocationRequestCommand(long ChatId) : ICommand;
