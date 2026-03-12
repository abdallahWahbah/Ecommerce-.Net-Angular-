using ECommerce.Application.Interfaces;
using ECommerce.Infrastructure.Persistence;
using ECommerce.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;


namespace ECommerce.Infrastructure.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJwtService, JwtService>();

            return services;
        }
    }
}
