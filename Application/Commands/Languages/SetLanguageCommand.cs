using Application.Common.Abstract;

namespace Application.Commands.Languages;

public record SetLanguageCommand(long UserId, string Language) : ICommand;
