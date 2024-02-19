using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure.HealthCheck;

public static class PostgreHealthCheck
{
    public static WebApplicationBuilder AddPostgreHealthCheck(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddHealthChecks()
            .AddNpgSql(builder.Configuration.GetConnectionString("Postgre")
                       ?? throw new InvalidOperationException(), tags: ["postgre"]);
        return builder;
    }
}