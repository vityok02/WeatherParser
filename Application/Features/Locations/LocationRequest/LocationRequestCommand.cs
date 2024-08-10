using Application.Abstract;

namespace Application.Features.Locations.LocationRequest;

public sealed record LocationRequestCommand(long ChatId) : ICommand;
