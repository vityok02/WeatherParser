using Application.Abstract;

namespace Application.Features.CallbackQueryCommands.SendGeolocationRequestCommand;

public record SendGeolocationRequestCommand(long UserId) : ICommand;
