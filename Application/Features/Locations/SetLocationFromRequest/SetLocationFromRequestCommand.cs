using Application.Abstract;
using Telegram.Bot.Types;

namespace Application.Locations.SetLocationFromRequest;

public record SetLocationFromRequestCommand(long UserId, Location Location) : ICommand;
