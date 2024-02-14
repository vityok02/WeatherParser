using Application.Abstract;

namespace Application.Features.Locations.SetLocation;

public record SetLocationCommand(long UserId, string Text) : ICommand;
