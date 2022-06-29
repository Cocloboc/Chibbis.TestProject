using System;
using System.Net;
using System.Threading.Tasks;
using Chibbis.TestProject.ProductManagementService.Application.Exceptions;
using Chibbis.TestProject.ProductManagementService.Web.Models;
using Microsoft.AspNetCore.Http;

namespace Chibbis.TestProject.ProductManagementService.Web.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            var error = new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error."
            };
            
            switch (exception.InnerException)
            {
                case NotFoundException e:
                {
                    context.Response.StatusCode = (int) HttpStatusCode.NotFound;

                    error.StatusCode = context.Response.StatusCode;
                    error.Message = e.Message;

                    break;
                }
            }

            await context.Response.WriteAsync(error.ToString());
        }
    }
}