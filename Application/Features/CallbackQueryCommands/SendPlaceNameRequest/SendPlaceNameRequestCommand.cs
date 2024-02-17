using Application.Abstract;

namespace Application.Features.CallbackQueryCommands.SendPlaceNameRequestCommand;

public record SendPlaceNameRequestCommand(long UserId) : ICommand;
