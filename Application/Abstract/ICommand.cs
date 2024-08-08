using Domain.Abstract;
using MediatR;

namespace Application.Abstract;

public interface ICommand : IRequest<Result>;