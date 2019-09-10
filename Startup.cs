[assembly: Microsoft.Azure.WebJobs.Hosting.WebJobsStartup(typeof(Func.Canary.Startup))]

namespace Func.Canary
{
    using System;
    using Func.Canary.Application;
    using Func.Canary.Extensions;
    using MediatR;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup : IWebJobsStartup
    {
        public IConfiguration Configuration { get; set; }

        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddExtension<ScopeCreepExtension>();
            builder.AddExtension<MediatrWarmupExtension>();

            AddServices(builder.Services);

            builder.Services.AddHttpClient<IScopeCreepService, ScopeCreepServiceClient>();
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(ScopeCreepCommandHandler));
            services.AddScoped<DependencyGraphAlpha>();
            services.AddScoped<DependencyGraphBravo>();
            services.AddScoped<GraphObjectA>();
            services.AddScoped<GraphObjectB>();
            services.AddScoped<IGraphInterfaceA>(sp => sp.GetRequiredService<GraphObjectA>());
        }
    }
}
