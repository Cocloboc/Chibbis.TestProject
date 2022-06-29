using Chibbis.TestProject.ProductManagementService.Application.Consumers;
using Chibbis.TestProject.ProductManagementService.Application.Services;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Chibbis.TestProject.ProductManagementService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();

            services.AddMediator();
            
            return services;
        }

        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediator(cfg =>
            {
                cfg.AddConsumersFromNamespaceContaining<RootConsumer>();
            });
            
            return services;
        }
    }
}