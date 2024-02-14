using Application.Abstract;

namespace Application.Features.Locations.EnterLocation;

public sealed record EnterLocationCommand(long UserId, string Text) : ICommand;
