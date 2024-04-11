using Application.Abstract;

namespace Application.Features.CallbackQueryCommands;

public record SendPlaceNameRequestCommand(long UserId) : ICommand;
