using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chibbis.TestProject.CartService.Application;
using Chibbis.TestProject.CartService.Application.Consumers.Cart.Commands.AddProducts;
using Chibbis.TestProject.CartService.Application.Services;
using Chibbis.TestProject.CartService.Web.Extensions;
using Chibbis.TestProject.CartService.Web.Filters;
using Chibbis.TestProject.CartService.Web.Middlewares;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace Chibbis.TestProject.CartService.Web
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
            
            services.AddFluentValidation(
                fv => fv.RegisterValidatorsFromAssemblyContaining<AddProductCommandValidator>());
            services.AddHttpContextAccessor();
            
            services.AddMassTransit(Configuration);
            services.AddRedis(Configuration);
            services.AddApplication(Configuration);
            services.AddOptions(Configuration);
            
            services.AddAutoMapper();
            
            services.AddHostedService<RedisEventsListener>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chibbis.TestProject.CartService.Web v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseMiddleware<UserContextMiddleware>();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}