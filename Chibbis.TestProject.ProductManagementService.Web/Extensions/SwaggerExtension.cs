using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Chibbis.TestProject.ProductManagementService.Web.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Chibbis.TestProject.CartService.Web", Version = "v1"});
            });
            services.AddFluentValidationRulesToSwagger();

            return services;
        }
    }
}