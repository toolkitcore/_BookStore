using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BookStore.Core.Helpers;

public static class ConfigurationHelper
{
    public static IConfiguration GetConfiguration(string? basePath)
    {
        basePath ??= Directory.GetCurrentDirectory();
        var builder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environments.Development}.json",
                true)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}