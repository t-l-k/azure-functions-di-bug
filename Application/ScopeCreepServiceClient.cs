using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Func.Canary.Application
{
    public class ScopeCreepServiceClient : IScopeCreepService
    {
        public HttpClient HttpClient { get; }

        public ScopeCreepServiceClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
    }

    public interface IScopeCreepService
    {
    }
}
