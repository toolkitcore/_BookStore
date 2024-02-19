using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BookStore.Infrastructure.HealthCheck;

public static class HealthCheckEndpointRouteBuilderExtensions
{
    public static void MapCustomHealthChecks(
        this WebApplication app,
        string healthPattern = "/hc",
        string livenessPattern = "/liveness",
        Func<HttpContext, HealthReport, Task> responseWriter = default!)
    {
        app.MapHealthChecks(healthPattern, new()
        {
            Predicate = _ => true,
            ResponseWriter = responseWriter,
            AllowCachingResponses = false,
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status200OK,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
            }
        });

        app.MapHealthChecks(livenessPattern, new()
        {
            Predicate = r => r.Name.Contains("self")
        });
    }
}