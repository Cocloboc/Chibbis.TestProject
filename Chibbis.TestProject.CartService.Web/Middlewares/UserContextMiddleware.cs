using System.Threading.Tasks;
using Chibbis.TestProject.CartService.Application.Services;
using Microsoft.AspNetCore.Http;

namespace Chibbis.TestProject.CartService.Web.Middlewares
{
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;

        public UserContextMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserContext userContext)
        {
            var isUserDefined = context.Request.Headers.TryGetValue("userUUID", out var userUUID);
            if (isUserDefined)
            {
                userContext.SetUserUUID(userUUID);
            }

            await _next.Invoke(context);
        }
    }
}