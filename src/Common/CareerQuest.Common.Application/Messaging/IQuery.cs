using CareerQuest.Common.Domain.Abstractions;
using MediatR;

namespace CareerQuest.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
