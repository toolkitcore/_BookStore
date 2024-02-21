using BookStore.Core.SharedKernel;

using MediatR;

namespace BookStore.Core.Abstractions;

public interface ICommand<out TResponse> : IRequest<TResponse>, ITransactionRequest;