using ECommerce.Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ECommerce.Application.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); // for FluentValidation
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>)); // for validation pipeline

            return services;
        }
    }
}
