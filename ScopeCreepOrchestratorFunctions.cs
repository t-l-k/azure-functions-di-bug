using Func.Canary.Application;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Func.Canary
{
    public class ScopeCreepOrchestratorFunction
    {

        [FunctionName("ScopeCreepOrchestrator")]
        public async Task<ScopeCreepActivityResponse> ScopeCreepOrchestrator([OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var request = context.GetInput<ScopeCreepActivityRequest>();

            var response = await context.CallActivityAsync<ScopeCreepActivityResponse>("ScopeCreepActivity", request);
            
            if (response.Depth < request.DepthRequested)
            {
                request = new ScopeCreepActivityRequest()
                {
                    Depth = request.Depth + 1,
                    DepthRequested = request.DepthRequested
                };

                response = await context.CallSubOrchestratorAsync<ScopeCreepActivityResponse>("ScopeCreepOrchestrator", request);

                if (response.Depth < request.DepthRequested)
                {
                    context.ContinueAsNew(request, true);
                }
            }
                       
            await context.CreateTimer(context.CurrentUtcDateTime.AddSeconds(5), CancellationToken.None);

            return response;
        }
    }
}
