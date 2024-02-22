using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog.Exceptions;
using Serilog.Settings.Configuration;
using Serilog;

namespace BookStore.Infrastructure.Logging;

public static class Extension
{
    public static void AddSerilog(this WebApplicationBuilder builder, string sectionName = "Serilog")
    {
        var serilogOptions = new SerilogOptions();
        builder.Configuration.GetSection(sectionName).Bind(serilogOptions);

        builder.Logging.ClearProviders();

        builder.Host.UseSerilog((context, config) =>
        {
            config.ReadFrom.Configuration(
                context.Configuration,
                new ConfigurationReaderOptions { SectionName = sectionName }
            );

            config
                .Enrich.WithProperty("Application", builder.Environment.ApplicationName)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails();

            if (serilogOptions.UseConsole)
                config.WriteTo.Async(writeTo =>
                    writeTo.Console(outputTemplate: serilogOptions.LogTemplate, theme: serilogOptions.ConsoleTheme));
        });
    }
}
