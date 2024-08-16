using Application.Abstract;

namespace Application.Features.Default;

public record DefaultCommand(long ChatId) : ICommand;
