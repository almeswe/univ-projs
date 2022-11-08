using System.Collections.Concurrent;

namespace DirectoryScanner.Core
{
    public abstract class ScanResult
    {
        protected long _bytes;
        protected bool _bytesRetrieved;

        public string Path { get; protected set; }
        public string Name { get; protected set; }
        public ConcurrentBag<ScanResult> Nodes { get; private set; }

        public ScanResult() =>
            this.Nodes = new ConcurrentBag<ScanResult>();

        public abstract long GetBytes(bool forceRecalculation = false);

        public virtual void AppendNode(ScanResult data) =>
            this.Nodes?.Add(data);
    }
}
