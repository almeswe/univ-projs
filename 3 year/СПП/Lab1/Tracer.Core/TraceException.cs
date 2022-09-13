using System;

namespace Tracer.Core
{
    public sealed class TraceException : Exception
    {
        public TraceException(string message) 
            : base(message) {}
    }
}