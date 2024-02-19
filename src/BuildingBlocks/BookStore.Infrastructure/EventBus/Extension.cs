using BookStore.Infrastructure.EventBus.Abstractions;
using BookStore.Infrastructure.EventBus.Dapr.Internal;
using BookStore.Infrastructure.EventBus.Dapr;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace BookStore.Infrastructure.EventBus;

public static class Extension
{
    public static IServiceCollection AddDaprClient(this IServiceCollection services,
        IConfiguration config)
    {
        services.Configure<DaprEventBusOptions>(config.GetSection(DaprEventBusOptions.Name));
        services.AddScoped<IEventBus, DaprEventBus>();

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        services.AddSingleton(options);

        services.AddDaprClient(client => client.UseJsonSerializationOptions(options));

        return services;
    }
}