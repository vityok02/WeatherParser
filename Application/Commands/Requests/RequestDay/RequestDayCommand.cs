using Application.Common.Abstract;

namespace Application.Commands.Requests.RequestDay;

public record RequestDayCommand(long UserId, string Text) : ICommand;
