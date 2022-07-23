using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Vrnz2.BaseInfra.Validations
{
    public static class ValiationServiceExtensions
    {
        public static IServiceCollection AddBaseValidations(this IServiceCollection services)
            => services
                    .AddScoped<IValidatorFactory, ValidatorFactory>()
                    .AddScoped<IValidationHelper, ValidationHelper>()
                    .AddScoped<ValidationHelper>();

        public static IServiceCollection AddValidation<TRequest, TResult>(this IServiceCollection services)
            where TRequest : class
            where TResult : AbstractValidator<TRequest>
        {
            services
                    .AddTransient<IValidator<TRequest>, TResult>();

            return services;
        }
    }
}