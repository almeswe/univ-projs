using System;
using System.Collections.Generic;

using Faker.Analyzer.Exceptions;

namespace Faker.Analyzer
{
    public sealed class GraphNet
    {
        public Graph Root { get; private set; }

        private readonly Dictionary<Type, Graph> TypeEntries;

        public GraphNet(Type root)
        {
            this.Root = new Graph(root);
            this.TypeEntries = new Dictionary<Type, Graph>();
        }

        public Graph AddGraphEdge(Graph to, Type type)
        {
            var graph = new Graph(type);
            to.AddNode(graph);
            if (!this.TypeEntries.ContainsKey(type))
                this.TypeEntries[type] = graph;
            else
            {
                if (this.GraphIsInProduction(graph, this.TypeEntries[type]))
                    throw new GraphNetCircularDependencyException(graph);
                this.TypeEntries[type] = graph;
            }
            return graph;
        }

        public bool GraphIsInProduction(Graph searchNode, Graph localRoot)
        {
            foreach (var edge in localRoot.Nodes)
            {
                if (edge.GetHashCode() == searchNode.GetHashCode())
                    return true;
                if (this.GraphIsInProduction(searchNode, edge))
                    return true;
            }
            return false;
        }
    }
}
