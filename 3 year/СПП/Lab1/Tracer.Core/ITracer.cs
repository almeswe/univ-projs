namespace Tracer.Core
{
    public interface ITracer
    {
        void StopTrace();
        void StartTrace();
        TraceResult GetTraceResult();
    }
}