namespace BookStore.Infrastructure.Outbox.Abstractions;

public interface ITransactionOutbox
{
    Task HandleAsync(Type message, CancellationToken token = default);
}