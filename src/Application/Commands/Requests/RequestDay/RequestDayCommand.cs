using Application.Common.Abstract;

namespace Application.Commands.Requests;

public sealed record RequestDayCommand(long UserId, string Day)
    : ICommand;
