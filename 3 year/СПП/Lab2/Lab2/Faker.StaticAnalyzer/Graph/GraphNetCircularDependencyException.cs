using System;
using System.Collections.Generic;

namespace Faker.Analyzer.Exceptions
{
    public sealed class GraphNetCircularDependencyException : GraphNetException
    {
        public Graph InGraph { get; private set; }

        public GraphNetCircularDependencyException(Graph inGraph) 
            : base("Circular dependency detected!")
        {
            this.InGraph = inGraph;
        }

        public Type[] GetTypeTrace()
        {
            var typeTrace = new List<Type>();
            for (var node = this.InGraph; node != null; node = node.Parent)
                typeTrace.Add(node.Type);
            typeTrace.Reverse();
            return typeTrace.ToArray();
        }
    }
}
