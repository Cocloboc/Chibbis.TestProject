using System;
using System.Collections.Generic;
using System.Linq;
using Chibbis.TestProject.CartService.Web.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Chibbis.TestProject.CartService.Web.Filters
{
    public class AuthorizationOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var attributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<Attribute>();


            foreach (var attribute in attributes)
            {
                operation.Security = attribute switch
                {
                    UserDataAttribute => new List<OpenApiSecurityRequirement>()
                    {
                        new()
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme, Id = "UserUUID"
                                    },
                                },
                                new List<string>()
                            }
                        }
                    },
                    _ => operation.Security
                };
            }
        }
    }
}