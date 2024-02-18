using BookStore.Infrastructure.EventBus.Abstractions;
using BookStore.Infrastructure.EventBus.Dapr.Internal;
using BookStore.Infrastructure.EventBus.Dapr;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure.EventBus;

public static class Extension
{
    public static IServiceCollection AddMessageBroker(this IServiceCollection services,
        IConfiguration config)
    {
        services.Configure<DaprEventBusOptions>(config.GetSection(DaprEventBusOptions.Name));
        services.AddScoped<IEventBus, DaprEventBus>();

        return services;
    }
}