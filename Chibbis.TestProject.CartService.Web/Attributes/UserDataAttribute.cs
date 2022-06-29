using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Chibbis.TestProject.CartService.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Chibbis.TestProject.CartService.Web.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class UserDataAttribute: Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue
                    ("userUUID", out var userUUID))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "User is undefined"
                };
                return;
            }

            await next();
        }
    }
}