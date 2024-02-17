using Application.Abstract;

namespace Application.Features.DefaultMessage;

public record DefaultBotCommand(long UserId) : ICommand;
