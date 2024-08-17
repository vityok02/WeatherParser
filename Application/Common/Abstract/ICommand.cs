using Domain.Abstract;
using MediatR;

namespace Application.Common.Abstract;

public interface ICommand : IRequest<Result>;