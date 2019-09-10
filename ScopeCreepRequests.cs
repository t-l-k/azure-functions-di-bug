using System;
using System.Collections.Generic;
using System.Text;

namespace Func.Canary
{
    public class ScopeCreepActivityRequest
    {
        public int Depth { get; set; } = 0;

        public int DepthRequested { get; set; } = 0; 
    }

    public class ScopeCreepActivityResponse
    {
        public int Depth { get; set; } = 0;
    }
}
