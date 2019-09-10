using Func.Canary.Application;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Func.Canary
{
    public class DependencyGraphAlpha
    {
        private static ulong objectNumber = 0L;
        private ulong objectInstance;

        public DependencyGraphAlpha(IGraphInterfaceA a, GraphObjectB b, IScopeCreepService creepService, ILogger<DependencyGraphAlpha> log)
        {
            objectInstance = objectNumber++; 

            log.LogTrace("[{0}] graphObjectA {1} (from {2} ({3})) {4}", objectInstance, a.GetHashCode(), GetHashCode(), GetType().Name, Thread.CurrentThread.ManagedThreadId);
            log.LogTrace("[{0}] graphObjectB {1} (from {2} ({3})) {4}", objectInstance, b.GetHashCode(), GetHashCode(), GetType().Name, Thread.CurrentThread.ManagedThreadId);
            A = a;
            B = b;
            CreepService = creepService;
            //Thread.Sleep(150);
        }

        public IGraphInterfaceA A { get; }
        public GraphObjectB B { get; }
        public IScopeCreepService CreepService { get; }
    }

    public class DependencyGraphBravo
    {
        private static ulong objectNumber = 0L;
        private ulong objectInstance;

        public DependencyGraphBravo(GraphObjectA a, GraphObjectB b, IScopeCreepService creepService, ILogger<DependencyGraphBravo> log)
        {
            objectInstance = objectNumber++;

            log.LogTrace("[{0}] graphObjectA {1} (from {2} ({3})) {4}", objectInstance, a.GetHashCode(), GetHashCode(), GetType().Name, Thread.CurrentThread.ManagedThreadId);
            log.LogTrace("[{0}] graphObjectB {1} (from {2} ({3})) {4}", objectInstance, b.GetHashCode(), GetHashCode(), GetType().Name, Thread.CurrentThread.ManagedThreadId);
            A = a;
            B = b;
            CreepService = creepService;
        }

        public GraphObjectA A { get; }
        public GraphObjectB B { get; }
        public IScopeCreepService CreepService { get; }
    }

    public class GraphObjectA : IGraphInterfaceA
    {
    }

    public class GraphObjectB
    {
    }

    public interface IGraphInterfaceA
    {
    }
}


