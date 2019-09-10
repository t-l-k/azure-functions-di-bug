using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Func.Canary.Extensions
{
    public class ScopeCreepExtension : IExtensionConfigProvider
    {
        private readonly IServiceProvider _provider;

        protected ILogger Log { get; }

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public ScopeCreepExtension(IServiceProvider provider, ILogger<ScopeCreepExtension> log)
        {
            Log = log;
            _provider = provider;
        }

        public void Initialize(ExtensionConfigContext context)
        {
            using (var scope = _provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dependency = services.GetService<DependencyGraphBravo>();

                Log.LogTrace("Doing 1st work with scoped dependency bravo: {0} a: {1} b: {2}", dependency.GetHashCode(), dependency.A.GetHashCode(), dependency.B.GetHashCode());
            }

            using (var scope = _provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dependency = services.GetService<DependencyGraphBravo>();
                
                Log.LogTrace("Doing 2nd work with scoped dependency bravo: {0} a: {1} b: {2}", dependency.GetHashCode(), dependency.A.GetHashCode(), dependency.B.GetHashCode());
            }
        }

    }
}