using BookStore.Core.SharedKernel;
using BookStore.Infrastructure.Outbox.Abstractions;
using BookStore.Infrastructure.Outbox.Dapr.Internal;
using BookStore.Infrastructure.Outbox.Dapr;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure.Outbox;

public static class Extension
{
    public static IServiceCollection AddOutbox(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<DaprOutboxOptions>(config.GetSection(DaprOutboxOptions.Name));
        services.AddScoped<INotificationHandler<DomainEventBase>, DaprDispatchedHandler>();
        services.AddScoped<ITransactionOutbox, DaprOutboxProcessor>();
        return services;
    }
}