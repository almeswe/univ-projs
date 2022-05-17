using System;
using System.Linq;
using System.Collections.Generic;

namespace DIContainer
{
    public sealed class ServiceGraphNet
    {
        public ServiceGraph Root { get; private set; }
        public List<ServiceGraph> Graphs { get; private set; }

        public ServiceGraphNet(ServiceGraph root)
        {
            this.Root = root;
            this.Graphs = new List<ServiceGraph>(){ this.Root };
        }

        public void Add(ServiceGraph graph) =>
            this.Graphs.Add(graph);

        public ServiceGraph Get(ServiceTypeInfo typeInfo) =>
            this.Graphs.FirstOrDefault(g => g.InstanceInfo.TypeInfo == typeInfo);

        public void Validate(ServiceGraph graph = null)
        {
            if (graph != null)
            {
                if (graph.IsMarked)
                    throw new ServiceGraphNetException("Cycled dependency detected."); 
                graph.IsMarked = true;
            }
            else
            {
                this.Graphs.ForEach(g => g.IsMarked = false);
                this.Root.IsMarked = true;
            }
            foreach (var edge in graph == null ? this.Root.Edges : graph.Edges)
                this.Validate(edge);
        }
    }
}