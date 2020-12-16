using Microsoft.Extensions.DependencyInjection;

namespace Vrnz2.BaseInfra.ServiceCollection
{
    public static class ServiceColletionExtensions
    {
        public static IServiceCollection AddIServiceColletion(this IServiceCollection services)
            => services.AddSingleton(services);
    }
}
