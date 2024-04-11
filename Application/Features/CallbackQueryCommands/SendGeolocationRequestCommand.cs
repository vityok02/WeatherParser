using Application.Abstract;

namespace Application.Features.CallbackQueryCommands;

public record SendGeolocationRequestCommand(long UserId) : ICommand;
