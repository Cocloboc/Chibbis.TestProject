using Chibbis.TestProject.CartService.Web.Filters;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Chibbis.TestProject.CartService.Web.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Chibbis.TestProject.CartService.Web", Version = "v1"});

                c.OperationFilter<AuthorizationOperationFilter>();

                c.AddSecurityDefinition("UserUUID", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = "userUUID",
                    Description = "UserUUID of the current user",
                });
            });
            services.AddFluentValidationRulesToSwagger();

            return services;
        }
    }
}