using Application.Abstract;

namespace Application.Features.Locations.DefaultUserState;

public record DefaultCommand(long ChatId) : ICommand;
