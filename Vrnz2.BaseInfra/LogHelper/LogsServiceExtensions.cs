using Vrnz2.BaseInfra.Saga;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace Vrnz2.BaseInfra.Logs
{
    public static class LogsServiceExtensions
    {
        public static ILogger Config(LogEventLevel minLogEventLevel = LogEventLevel.Information)
            => Log.Logger = new LoggerConfiguration()
                                 .MinimumLevel.Is(minLogEventLevel)
                                 .WriteTo.Console(new JsonFormatter(), minLogEventLevel)
                                 .CreateLogger();

        public static IServiceCollection AddLogs(this IServiceCollection services, out ILogger logger, LogEventLevel minLogEventLevel = LogEventLevel.Information)
        {
            var newLogger = Config(minLogEventLevel).ForContext<ILogger>();

            logger = newLogger;

            return services
                .AddSingleton(_ => newLogger)
                .AddScoped<ISagaHandler, SagaHandler>();
        }

        public static IServiceCollection AddLogs(this IServiceCollection services, ILogger logger)
            => services
                .AddSingleton(_ => logger.ForContext<ILogger>())
                .AddScoped(_ => new SagaHandler());
    }
}
