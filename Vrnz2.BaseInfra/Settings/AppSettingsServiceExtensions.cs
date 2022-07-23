using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Vrnz2.BaseContracts.Settings.Base;

namespace Vrnz2.BaseInfra.Settings
{
    public static class AppSettingsServiceExtensions
    {
        public static IServiceCollection AddSettings<TSettings>(this IServiceCollection services, string jsonAttributeName)
            where TSettings : BaseAppSettings
            => services
                .Configure<TSettings>(ConfigurationFactory.Instance.Configuration.GetSection(jsonAttributeName));

        public static IServiceCollection AddSettings<TSettings>(this IServiceCollection services)
            where TSettings : BaseAppSettings
            => services
                .Configure<TSettings>(ConfigurationFactory.Instance.Configuration);

        public static IServiceCollection AddSettings<TSettings>(this IServiceCollection services, out TSettings settings)
            where TSettings : BaseAppSettings
        {
            settings = services
                .Configure<TSettings>(ConfigurationFactory.Instance.Configuration)
                .BuildServiceProvider()
                .GetService<IOptions<TSettings>>().Value;

            return services;
        }

        public static IServiceCollection AddSettings<TSettings>(this IServiceCollection services, IConfiguration configuration, out TSettings appSettings)
            where TSettings : BaseAppSettings
        {
            var settings = (TSettings)Activator.CreateInstance(typeof(TSettings));

            new ConfigureFromConfigurationOptions<TSettings>(configuration)
                .Configure(settings);

            appSettings = settings;

            return services;
        }
    }
}
