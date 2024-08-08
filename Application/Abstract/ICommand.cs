using Domain.Abstract;
using MediatR;
using Telegram.Bot.Types;

namespace Application.Abstract;

public interface ICommand : IRequest<Result>;