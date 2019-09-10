using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Func.Canary.Application
{
    public class ScopeCreepCommand : IRequest<ScopeCreepCommandResult>
    {
        public int Depth { get; set; }
    }
}
