using Chibbis.TestProject.CartService.Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis.Extensions.Core.Configuration;

namespace Chibbis.TestProject.CartService.Web.Extensions
{
    public static class OptionsExtension
    {
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CartOptions>(configuration.GetSection(CartOptions.Section));
            services.Configure<ReportOptions>(configuration.GetSection(ReportOptions.Section));
            services.Configure<RedisConfiguration>(configuration.GetSection("Redis"));

            return services;
        }
    }
}