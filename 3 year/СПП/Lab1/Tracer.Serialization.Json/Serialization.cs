using System.IO;
using System.Text;

using Newtonsoft.Json.Linq;

using Tracer.Core;
using Tracer.Serialization.Abstractions;

namespace Tracer.Serialization.Json
{
    public sealed class Serializer : ITraceResultSerializer
    {
        public void Serialize(TraceResult traceResult, Stream to)
        {
            var jsonObject = new JObject();
            var threadArray = new JArray();
            foreach (var key in traceResult.Tree.Roots.Keys)
            {
                var threadObject = new JObject();
                threadObject.Add("id", key);
                threadObject.Add("time", $"{traceResult.Tree.GetThreadSummary(key).TotalMilliseconds} ms");
                var threadMethods = new JArray();
                foreach (var rootNode in traceResult.Tree.Roots[key])
                    threadMethods.Add(this.SerializeNode(rootNode));
                threadObject.Add("methods", threadMethods);
                threadArray.Add(threadObject);
            }
            jsonObject.Add("threads", threadArray);
            to.Write(Encoding.UTF8.GetBytes(jsonObject.ToString()));
        }

        private JObject SerializeNode(CallNode node)
        {
            var jObject = new JObject();
            jObject.Add("name", node.Note.Method.Name);
            jObject.Add("class", node.Note.Class.Name);
            jObject.Add("time", $"{node.Note.ExecutionTime.Milliseconds} ms");
            var methods = new JArray();
            foreach (var subNode in node.Nodes)
                methods.Add(this.SerializeNode(subNode));
            jObject.Add("methods", methods);
            return jObject;
        }
    }
}
