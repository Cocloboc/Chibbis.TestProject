using Chibbis.TestProject.ProductManagementService.Application.MappingProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace Chibbis.TestProject.ProductManagementService.Web.Extensions
{
    public static class AutomapperExtension
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductProfile));
            
            return services;
        }
    }
}