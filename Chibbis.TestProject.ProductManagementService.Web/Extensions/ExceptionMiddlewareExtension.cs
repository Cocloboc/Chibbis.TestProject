using System.Net;
using Chibbis.TestProject.ProductManagementService.Application.Exceptions;
using Chibbis.TestProject.ProductManagementService.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Chibbis.TestProject.ProductManagementService.Web.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature == null)
                    {
                        return;
                    }

                    var error = new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "Internal Server Error."
                    };

                    if (contextFeature.Error is MassTransit.RequestException ex)
                    {
                        switch (ex.InnerException)
                        {
                            case NotFoundException e:
                            {
                                context.Response.StatusCode = (int) HttpStatusCode.NotFound;

                                error.StatusCode = context.Response.StatusCode;
                                error.Message = e.Message;

                                break;
                            }
                            case HasDuplicateExceptions e:
                            {
                                error.Message = e.Message;

                                break;
                            }
                        }
                    }

                    await context.Response.WriteAsync(error.ToString());
                });
            });
        }
    }
}