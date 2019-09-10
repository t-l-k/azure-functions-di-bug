using Func.Canary.Application;
using MediatR;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Func.Canary.Extensions
{
    [Extension("MediatrWarmupExtension")]
    public class MediatrWarmupExtension : IExtensionConfigProvider
    {
        public ILogger Log { get; }

        public MediatrWarmupExtension(IServiceProvider serviceProvider, ILogger<MediatrWarmupExtension> log)
        {
            //_serviceProvider = serviceProvider;

            Log = log;
            WarmUpMediatr(serviceProvider);
        }

        public void WarmUpMediatr(IServiceProvider provider)
        {
            // creating a subscope doesn't work, doesn't matter anyhow as we get passed a scoped service provider.
            //using (var scope = provider.CreateScope())
            //{
            //var scopedProvider = scope.ServiceProvider;
            var scopedProvider = provider;

            // What if we take all the handlers and "warm them up" by resolving them all from the container, within a scope, 
            // then throwing them all away? 
            // Perhaps that will fix it.

            // Get the same type 3 times, what happens? 
            // Warmup ...

            var handler1 = scopedProvider.GetRequiredService<IRequestHandler<ScopeCreepCommand, ScopeCreepCommandResult>>();
            var handler2 = scopedProvider.GetRequiredService<IRequestHandler<ScopeCreepCommand, ScopeCreepCommandResult>>();
            // ^^ The bug manifests at this point ^^ handler1 and handler2 dependencies should be the same. 
            // The "DependencyGraphAlpha" instance is new and has new dependencies also. 
            // But "DependencyGraphBravo" instance is scoped the same and has same dependencies. 

            // The problem is with the injection of a HttpClient. 
                       
            // The above dependencies are all registered as scoped.

            // Surplus to requirements:
            // var handler3 = scopedProvider.GetRequiredService<IRequestHandler<ScopeCreepCommand, ScopeCreepCommandResult>>();

            var assembly = typeof(ScopeCreepCommandHandler).Assembly;

            foreach (var assemblyType in assembly.GetTypes())
            {
                foreach (var candidateInterface in assemblyType.GetInterfaces())
                {
                    if (candidateInterface.IsGenericType
                        && candidateInterface.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                    {
                        // Warm this type up from the service provider ...
                        var handler = scopedProvider.GetRequiredService(candidateInterface);
                        Log.LogInformation("Warmed up IRequestHandler for {0}", candidateInterface.GetGenericArguments()[0]);
                    }
                }
            }
        }

        public void Initialize(ExtensionConfigContext context)
        {
            //WarmUpMediatr(_serviceProvider);

            // throw new NotImplementedException();
        }
    }
}
