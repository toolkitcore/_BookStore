using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure.HealthCheck;

public static class Extension
{
    public static IHealthChecksBuilder AddDapr(this IHealthChecksBuilder builder) 
        => builder.AddCheck<DaprHealthCheck>("dapr");
}