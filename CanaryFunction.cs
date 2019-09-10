using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Hosting;

namespace Func.Canary
{
    public class CanaryFunction
    {
        [FunctionName("CanaryFunctionAnon")]
        public static async Task<IActionResult> RunAnonymous(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "CanaryAnon")] HttpRequest req,
            ILogger log)
        {
            var result = new { canary = "tweet" };

            log.LogInformation(JsonConvert.SerializeObject(result));

            return new OkObjectResult(result);
        }

        [FunctionName("CanaryFunctionAdmin")]
        public static async Task<IActionResult> RunAdmin(
           [HttpTrigger(AuthorizationLevel.Admin, "get", "post", Route = "CanaryAdmin")] HttpRequest req,
           ILogger log)
        {
            return await RunAnonymous(req, log);
        }

      
    }
}
