using BookStore.Core.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Diagnostics;

namespace BookStore.Persistence;

[DebuggerStepThrough]
public sealed class TransactionBehavior<TRequest, TResponse>(
    IPublisher publisher,
    IDatabaseFacade databaseFacade,
    IDomainEventContext eventContext,
    ILogger<TransactionBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is not ITransactionRequest)
            return await next();

        logger.LogInformation("{Request} handled command {CommandName}", request, request.GetType().Name);
        logger.LogDebug("{Request} handled command {CommandName} with {CommandData}", request, request.GetType().Name, request);
        logger.LogInformation("{Request} begin transaction for command {CommandName}", request, request.GetType().Name);

        var strategy = databaseFacade.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await databaseFacade.Database
                .BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

            logger.LogInformation("{Request} transaction {TransactionId} begin for command {CommandName}", request, transaction.TransactionId, request.GetType().Name);

            var domainEvents = eventContext.GetDomainEvents().ToList();

            logger.LogInformation("{Request} transaction {TransactionId} begin for command {CommandName} with {DomainEventsCount} domain events", request, transaction.TransactionId, request.GetType().Name, domainEvents.Count);

            var tasks = domainEvents.Select(async 
                domainEvent => await publisher.Publish(domainEvent, cancellationToken));

            await Task.WhenAll(tasks).ConfigureAwait(false);

            var response = await next();
            await transaction.CommitAsync(cancellationToken);
            return response;
        }).ConfigureAwait(false);
    }
}