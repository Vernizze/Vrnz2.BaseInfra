using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Vrnz2.BaseInfra.Validations
{
    public static class ValiationServiceExtensions
    {
        public static IServiceCollection AddBaseValidations(this IServiceCollection services)
            => services
                    .AddScoped<IValidatorFactory, ValidatorFactory>()
                    .AddScoped<ValidationHelper>();

        public static IServiceCollection AddValidation<TRequest, TResult>(this IServiceCollection services)
            where TRequest : class, IValidator
            where TResult : AbstractValidator<TRequest>
        {
            services
                    .AddTransient<IValidator<TRequest>, TResult>();

            return services;
        }
    }
}