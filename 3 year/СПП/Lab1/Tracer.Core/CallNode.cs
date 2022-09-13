using System.Diagnostics;

using System.Collections.Generic;

namespace Tracer.Core
{
    public sealed class CallNode
    {
        public TraceNote Note { get; internal set; }
        public StackTrace Trace { get; internal set; }
        public List<CallNode> Nodes { get; private set; }
        public string MethodName => this.Note.Method.Name;

        public CallNode(StackTrace trace)
        {
            this.Trace = trace;
            this.Nodes = new List<CallNode>();
            this.Note = new TraceNote(trace);
        }

        public void SetTimeStamp(Stopwatch watch) =>
            this.Note.ExecutionTime = watch.Elapsed;
    }
}