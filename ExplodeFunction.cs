using Func.Canary.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Func.Canary
{
    public class ExplodeFunction
    {
        public ExplodeFunction(ILogger<ExplodeFunction> log)
        {
            Log = log;
        }

        public ILogger<ExplodeFunction> Log { get; }

        [FunctionName("DependencyExplode")]
        public async Task<IActionResult> DependencyExplode(
             [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "DependencyExplode")] HttpRequest req,
             [OrchestrationClient] DurableOrchestrationClient starter
             )
        {
            // Just use a simple command to kick off a couple of orchestrators ..
            var requestOne = new ScopeCreepActivityRequest() { DepthRequested = 2 };
            var requestTwo = new ScopeCreepActivityRequest() { DepthRequested = 2 };

            var instanceOne = await starter.StartNewAsync("ScopeCreepOrchestrator", requestOne);
            var instanceTwo = await starter.StartNewAsync("ScopeCreepOrchestrator", requestTwo);

            Log.LogTrace("orchestration one: {0}", instanceOne);
            Log.LogTrace("orchestration two: {0}", instanceTwo);

            // var payload = await starter.GetStatusAsync(instanceId);
            return new OkResult();
        }
    }
}
