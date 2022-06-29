using Chibbis.TestProject.Contracts.Product.Queries;
using Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Queries.GetProducts;
using Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Queries.ValidateProducts;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chibbis.TestProject.ProductManagementService.Web.Extensions
{
    public static class MassTransitExtension
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<ValidateProductsQueryConsumer>();
                x.AddConsumer<GetProductsQueryConsumer>();
                x.SetKebabCaseEndpointNameFormatter();
                
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
            services.AddMassTransitHostedService();

            return services;
        }
    }
}