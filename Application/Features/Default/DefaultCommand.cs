using Application.Abstract;

namespace Application.Default;

public record DefaultCommand(long UserId) : ICommand;
