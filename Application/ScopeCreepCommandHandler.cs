using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Func.Canary.Application
{
    public class ScopeCreepCommandHandler : IRequestHandler<ScopeCreepCommand, ScopeCreepCommandResult>
    {
        public DependencyGraphAlpha GraphAlpha { get; set; }
        public DependencyGraphBravo GraphBravo { get; set; }

        public ScopeCreepCommandHandler(DependencyGraphAlpha alpha, DependencyGraphBravo bravo)
        {
            // This is where the injected dependencies go a bit skew-iffed
            GraphAlpha = alpha;
            GraphBravo = bravo;
        }
        
        // Object 1 has some registered dependencies 

        // Object 2 has the same registered dependencies, but first time through, they're different. 

        // Doesn't manifest in an orchestration ...??

        public async Task<ScopeCreepCommandResult> Handle(ScopeCreepCommand request, CancellationToken cancellationToken)
        {
            // Do some work here ...
            Console.WriteLine("Doing some application work ... depth {0}", request.Depth);

            return new ScopeCreepCommandResult(); 
        }
    }
}