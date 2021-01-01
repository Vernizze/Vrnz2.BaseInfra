using Microsoft.Extensions.DependencyInjection;
using Vrnz2.BaseContracts.Settings.Base;

namespace Vrnz2.BaseInfra.Settings
{
    public static class AppSettingsServiceExtensions
    {
        public static IServiceCollection AddSettings<T>(this IServiceCollection services, string jsonAttributeName)
            where T : BaseAppSettings
            => services
                .Configure<T>(ConfigurationFactory.Instance.Configuration.GetSection(jsonAttributeName));

        public static IServiceCollection AddSettings<T>(this IServiceCollection services)
            where T : BaseAppSettings
            => services
                .Configure<T>(ConfigurationFactory.Instance.Configuration);

        public static IServiceCollection AddSettings<T>(this IServiceCollection services, out T settings)
            where T : BaseAppSettings
        {
            settings = services
                .Configure<T>(ConfigurationFactory.Instance.Configuration)
                .BuildServiceProvider()
                .GetService<T>();

            return services;
        }
    }
}
