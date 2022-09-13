using System.IO;
using System.Xml.Linq;

using Tracer.Core;
using Tracer.Serialization.Abstractions;

namespace Tracer.Serialization.Xml
{
    public sealed class Serializer : ITraceResultSerializer
    {
        public void Serialize(TraceResult traceResult, Stream to)
        {
            var doc = new XDocument();
            var root = new XElement("root");
            doc.Add(root);
            foreach (var key in traceResult.Tree.Roots.Keys)
            {
                var thread = new XElement("thread");
                thread.Add(new XAttribute("id", $"{key}"));
                thread.Add(new XAttribute("time", $"{traceResult.Tree.GetThreadSummary(key).TotalMilliseconds} ms"));
                foreach (var rootNode in traceResult.Tree.Roots[key])
                    thread.Add(this.SerializeNode(rootNode));
                doc.Root.Add(thread);
            }
            doc.Save(to);
        }

        private XElement SerializeNode(CallNode node)
        {
            var nodeElement = new XElement("method");
            nodeElement.Add(new XAttribute("name", node.Note.Method.Name));
            nodeElement.Add(new XAttribute("class", node.Note.Class.Name));
            nodeElement.Add(new XAttribute("time", $"{node.Note.ExecutionTime.TotalMilliseconds} ms"));
            foreach (var subNode in node.Nodes)
                nodeElement.Add(this.SerializeNode(subNode));
            return nodeElement;
        }
    }
}
