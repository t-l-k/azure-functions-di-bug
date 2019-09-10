using Func.Canary.Application;
using MediatR;
using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;

namespace Func.Canary
{
    public class ScopeCreepActivityFunctions
    {
        private readonly IMediator _mediator;

        public ScopeCreepActivityFunctions(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("ScopeCreepActivity")]
        public async Task<ScopeCreepActivityResponse> ScopeCreepActivity(
            [ActivityTrigger] ScopeCreepActivityRequest request)
        {
            // Create a command that should demonstrate this weird "scope creep" 
            var command = new ScopeCreepCommand() { Depth = request.Depth };

            // Await Mediatr to execute it 
            var result = await _mediator.Send(command);

            // Return result 
            return new ScopeCreepActivityResponse() { Depth = request.Depth };
        }
    }
}