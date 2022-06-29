using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace Chibbis.TestProject.CartService.Web.Extensions
{
    public static class RedisExtension
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConfiguration = configuration.GetSection("Redis").Get<RedisConfiguration>();
            
            services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(redisConfiguration);
            
            return services;
        }
    }
}