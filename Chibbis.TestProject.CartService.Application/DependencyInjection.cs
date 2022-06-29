using Chibbis.TestProject.CartService.Application.Consumers;
using Chibbis.TestProject.CartService.Application.Extensions;
using Chibbis.TestProject.CartService.Application.Filters;
using Chibbis.TestProject.CartService.Application.Jobs;
using Chibbis.TestProject.CartService.Application.Options;
using Chibbis.TestProject.CartService.Application.Services;
using Chibbis.TestProject.CartService.Application.Services.ReportService;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Chibbis.TestProject.CartService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediator();
            services.AddQuartz(configuration);

            services.AddScoped<ICartService, Services.CartService>();
            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<IWebhookService, WebhookService>();
            services.AddScoped<IReportService, ReportService>();

            services.AddHttpClients();

            return services;
        }

        private static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediator(cfg =>
            {
                cfg.AddConsumersFromNamespaceContaining<RootConsumer>();

                cfg.ConfigureMediator((context, cfg) => cfg.UseHttpContextScopeFilter(context));
            });

            return services;
        }

        private static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient<IWebhookService, WebhookService>(c => { });

            return services;
        }

        private static IServiceCollection AddQuartz(this IServiceCollection services, IConfiguration configuration)
        {
            var reportOptions = new ReportOptions();
            configuration.GetSection(ReportOptions.Section).Bind(reportOptions);
            
            services.AddQuartz(q =>  
            {
                q.UseMicrosoftDependencyInjectionScopedJobFactory();
                
                var jobKey = new JobKey("ReportsJob");
                
                q.AddJob<ReportJob>(opts => opts.WithIdentity(jobKey));
                
                q.AddTrigger(opts => opts
                    .ForJob(jobKey) 
                    .WithIdentity("ReportsJob-trigger") 
                    .WithCronSchedule(reportOptions.Schedule)); 

            });
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
            
            return services;
        }
    }
}