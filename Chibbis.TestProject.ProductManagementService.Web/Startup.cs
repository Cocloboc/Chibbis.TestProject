using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chibbis.TestProject.ProductManagementService.Application;
using Chibbis.TestProject.ProductManagementService.Application.Consumers.Product.Commands.CreateProduct;
using Chibbis.TestProject.ProductManagementService.Web.Extensions;
using Chibbis.TestProject.ProductManagementService.Web.Middlewares;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace Chibbis.TestProject.ProductManagementService.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwagger();

            services.AddRedis(Configuration);
            services.AddMassTransit(Configuration);
            services.AddAutoMapper();
            services.AddFluentValidation(
                fv => fv.RegisterValidatorsFromAssemblyContaining<CreateProductCommandValidator>());
            
            services.AddApplication();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Chibbis.TestProject.ProductManagementService.Web v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}