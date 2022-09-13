using System.IO;

using Tracer.Core;
using Tracer.Serialization.Abstractions;

using YamlDotNet.RepresentationModel;

namespace Tracer.Serialization.Yaml
{
    public sealed class Serializer : ITraceResultSerializer
    {
        public void Serialize(TraceResult traceResult, Stream to)
        {
            var threadsNode = new YamlSequenceNode();
            var mainNode = new YamlMappingNode(
                new YamlScalarNode("threads"), threadsNode);
            foreach (var key in traceResult.Tree.Roots.Keys)
            {
                var threadMethods = new YamlSequenceNode();
                var sequenceNode = new YamlMappingNode(
                    new YamlScalarNode("id"), 
                    new YamlScalarNode($"{key}"),
                    new YamlScalarNode("time"),
                    new YamlScalarNode($"{traceResult.Tree.GetThreadSummary(key).TotalMilliseconds} ms"),
                    new YamlScalarNode("methods"),
                    threadMethods);
                foreach (var rootNode in traceResult.Tree.Roots[key])
                    threadMethods.Add(this.SerializeNode(rootNode));
                threadsNode.Add(sequenceNode);
            }

            using (var writer = new StreamWriter(to))
            {
                new YamlStream(new YamlDocument(mainNode))
                    .Save(writer, false);
            }
        }

        private YamlMappingNode SerializeNode(CallNode node)
        {
            var methods = new YamlSequenceNode();
            var mappingNode = new YamlMappingNode(
                new YamlScalarNode("name"),
                new YamlScalarNode($"{node.Note.Method.Name}"),
                new YamlScalarNode("class"),
                new YamlScalarNode($"{node.Note.Class.Name}"),
                new YamlScalarNode("time"),
                new YamlScalarNode($"{node.Note.ExecutionTime.TotalMilliseconds} ms"),
                new YamlScalarNode("methods"),
                methods);
            foreach (var subNode in node.Nodes)
                methods.Add(this.SerializeNode(subNode));
            return mappingNode;
        }
    }
}
