using System;
using System.Collections.Generic;

namespace Faker.Analyzer
{
    public sealed class Graph
    {
        public Type Type { get; private set; }
        public Graph Parent { get; private set; }
        public List<Graph> Nodes { get; private set; }

        public Graph(Type type)
        {
            this.Type = type;
            this.Parent = null;
            this.Nodes = new List<Graph>();
        }

        public void AddNode(Graph node)
        {
            this.Nodes.Add(node);
            node.Parent = this;
        }
    }
}