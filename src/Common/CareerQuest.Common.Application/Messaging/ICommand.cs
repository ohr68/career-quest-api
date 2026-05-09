using CareerQuest.Common.Domain.Abstractions;
using MediatR;

namespace CareerQuest.Common.Application.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

public interface IBaseCommand;
