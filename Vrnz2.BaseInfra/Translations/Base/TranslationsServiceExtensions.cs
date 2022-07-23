using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Vrnz2.BaseInfra.Assemblies;
using Vrnz2.BaseInfra.MessageCodes;

namespace Vrnz2.BaseInfra.Translations.Base
{
    public static class TranslationsServiceExtensions
    {
        public static IServiceCollection AddTranslations(this IServiceCollection services)
        {
            MessageCodesFactory.Instance.InitMessages(AssembliesHelper.GetAssemblies<BaseLocaleMessages>());

            return services;
        }

        public static IServiceCollection AddTranslations(this IServiceCollection services, Assembly assembly)
        {
            MessageCodesFactory.Instance.InitMessages(assembly);

            return services;
        }
    }
}
