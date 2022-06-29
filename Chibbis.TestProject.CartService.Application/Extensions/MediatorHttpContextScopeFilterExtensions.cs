using System;
using Chibbis.TestProject.CartService.Application.Filters;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Chibbis.TestProject.CartService.Application.Extensions
{
    public static class MediatorHttpContextScopeFilterExtensions
    {
        public static void UseHttpContextScopeFilter(this IMediatorConfigurator configurator, IServiceProvider serviceProvider)
        {
            var filter = new HttpContextScopeFilter(serviceProvider.GetRequiredService<IHttpContextAccessor>());

            configurator.ConfigurePublish(x => x.UseFilter(filter));
            configurator.ConfigureSend(x => x.UseFilter(filter));
            configurator.UseFilter(filter);
        }
    }
}