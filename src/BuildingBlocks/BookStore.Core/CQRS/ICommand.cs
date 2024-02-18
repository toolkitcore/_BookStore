using BookStore.Core.SharedKernel;
using MediatR;

namespace BookStore.Core.CQRS;

public interface ICommand<out TResponse> : IRequest<TResponse>, ITransactionRequest;