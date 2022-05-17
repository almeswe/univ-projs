using System.Linq;
using System.Collections.Generic;

namespace DIContainer
{
    public sealed class ServiceGraph
    {
        public bool IsMarked { get; set; }
        public bool IsEmpty { get => Edges.Count == 0; }
        public List<ServiceGraph> Edges { get; private set; }
        public ServiceInstanceInfo InstanceInfo { get; private set; }

        public ServiceGraph(ServiceTypeInfo typeInfo)
        {
            this.Edges = new List<ServiceGraph>();
            this.InstanceInfo = new ServiceInstanceInfo(typeInfo);
        }

        public void AddEdges(params ServiceGraph[] edges) =>
            this.Edges?.AddRange(edges.ToList());
    }
}