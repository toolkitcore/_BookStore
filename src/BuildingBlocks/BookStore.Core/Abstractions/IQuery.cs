using MediatR;

namespace BookStore.Core.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>;