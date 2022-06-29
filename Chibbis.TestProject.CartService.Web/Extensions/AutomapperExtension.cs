using Chibbis.TestProject.CartService.Application.MappingProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace Chibbis.TestProject.CartService.Web.Extensions
{
    public static class AutomapperExtension
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(WebhookProfile));
            
            return services;
        }
    }
}