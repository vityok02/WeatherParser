using Application.Common.Abstract;

namespace Application.Commands.Requests.RequestLanguage;

public record RequestLanguageCommand(long UserId) : ICommand;
