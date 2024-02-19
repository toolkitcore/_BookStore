using Serilog.Sinks.SystemConsole.Themes;

namespace BookStore.Infrastructure.Logging;

public sealed class SerilogOptions
{
    public bool UseConsole { get; set; } = true;

    public string LogTemplate { get; set; } =
        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level} - {Message:lj}{NewLine}{Exception}";

    public ConsoleTheme ConsoleTheme { get; set; } = AnsiConsoleTheme.Literate;
}