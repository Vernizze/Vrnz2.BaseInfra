using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Vrnz2.BaseInfra.Logs
{
    public static class LogsServiceExtensions
    {
        public static ILogger Config(LogEventLevel minLogEventLevel = LogEventLevel.Information, string outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            => Log.Logger = new LoggerConfiguration()
                   .MinimumLevel.Is(minLogEventLevel)
                   .WriteTo.Console(minLogEventLevel, outputTemplate: outputTemplate, theme: AnsiConsoleTheme.Code)
                   .CreateLogger();

        public static IServiceCollection AddLogs(this IServiceCollection services, LogEventLevel minLogEventLevel = LogEventLevel.Information, string outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            => services.AddSingleton(_ => Config(minLogEventLevel, outputTemplate).ForContext<ILogger>());

        public static IServiceCollection AddLogs(this IServiceCollection services, ILogger logger, LogEventLevel minLogEventLevel = LogEventLevel.Information, string outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            => services.AddSingleton(_ => logger.ForContext<ILogger>());
    }
}
