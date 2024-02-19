using Dapr.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BookStore.Infrastructure.HealthCheck;

public class DaprHealthCheck(DaprClient daprClient) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
        => await daprClient.CheckHealthAsync(cancellationToken)
            ? HealthCheckResult.Healthy()
            : new(context.Registration.FailureStatus, "Dapr is unhealthy");
}