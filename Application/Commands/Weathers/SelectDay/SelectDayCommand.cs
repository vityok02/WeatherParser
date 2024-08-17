using Application.Common.Abstract;

namespace Application.Commands.Weathers.SelectDay;

public sealed record SelectDayCommand(long ChatId, string Day)
    : ICommand;
