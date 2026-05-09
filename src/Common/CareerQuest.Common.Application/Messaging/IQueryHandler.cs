using CareerQuest.Common.Domain.Abstractions;
using MediatR;

namespace CareerQuest.Common.Application.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
