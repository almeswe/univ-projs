namespace Tracer.Core
{
    public struct TraceResult
    {
        public readonly CallTree Tree { get; }

        public TraceResult(CallTree tree) =>
            this.Tree = tree;
    }
}