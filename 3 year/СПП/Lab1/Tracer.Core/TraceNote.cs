using System;
using System.Threading;
using System.Reflection;
using System.Diagnostics;

namespace Tracer.Core
{
    public sealed class TraceNote
    {
        public int ThreadId { get; private set; }
        
        public Type Class { get; private set; }
        public MethodBase Method { get; private set; }
        
        public TimeSpan ExecutionTime { get; internal set; }

        public TraceNote(StackTrace trace)
        {
            this.Method = trace.GetFrame(1).GetMethod();
            this.Class = this.Method.DeclaringType;
            this.ThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        public override string ToString() =>
            $"Thread id [{this.ThreadId}]\r\nMethod    [{this.Method.Name}]\r\n" +
            $"Class     [{this.Class.Name}]\r\nTime      [{this.ExecutionTime}]";
    }
}