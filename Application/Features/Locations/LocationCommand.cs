using Application.Abstract;

namespace Application.Features.Locations;

public sealed record LocationCommand(long UserId) : ICommand;
