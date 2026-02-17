using Application.Common.Abstract;

namespace Application.Commands.Requests.RequestLocation;

public sealed record RequestLocationCommand(long UserId) : ICommand;
