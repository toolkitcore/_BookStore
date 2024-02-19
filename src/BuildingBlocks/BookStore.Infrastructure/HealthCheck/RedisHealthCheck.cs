using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure.HealthCheck;

public static class RedisHealthCheck
{
    public static WebApplicationBuilder AddRedisHealthCheck(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddHealthChecks()
            .AddRedis(builder.Configuration.GetConnectionString("Redis")
                                  ?? throw new InvalidOperationException(), tags: ["redis"]);
        return builder;
    }
}