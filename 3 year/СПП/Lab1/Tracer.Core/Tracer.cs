using System;
using System.Diagnostics;

using System.Collections.Generic;

namespace Tracer.Core
{
    public sealed class Tracer : ITracer
    {
        private readonly object _locker;

        private readonly CallTree _tree;
        private readonly Stack<Tuple<CallNode, Stopwatch>> _frames;

        public Tracer()
        {
            this._locker = new object();
            this._tree = new CallTree();
            this._frames = new Stack<Tuple<CallNode, Stopwatch>>();
        }

        public TraceResult GetTraceResult() =>
            new TraceResult(this._tree);

        public void StartTrace()
        {
            var watch = new Stopwatch();
            var trace = new StackTrace();
            var node = new CallNode(trace);
            lock (this._locker) 
            {
                this._tree.AddNode(node);
                this._frames.Push(Tuple.Create(node, watch));
                watch.Start();
            };
        }

        public void StopTrace()
        {
            lock (this._locker)
            {
                var (node, watch) = this._frames.Pop();
                watch.Stop();
                node.SetTimeStamp(watch);
            }
        }
    }
}