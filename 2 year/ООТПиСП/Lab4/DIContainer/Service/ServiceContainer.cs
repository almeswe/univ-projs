using System;
using System.Linq;
using System.Collections.Generic;

namespace DIContainer
{
    public sealed class ServiceContainer
    {
        private List<Service> _services;
        private List<ServiceTypeInfo> _registered;

        public ServiceContainer()
        {
            this._services = new List<Service>();
            this._registered = new List<ServiceTypeInfo>();
        }

        public ServiceContainer Register<TInterface, TImplementation>(
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var interfaceType = typeof(TInterface);
            var implementationType = typeof(TImplementation);
            var serviceInfo = new ServiceTypeInfo(interfaceType, 
                implementationType, lifetime);
            var existingServiceInfo = this._registered.FirstOrDefault(
                r => r == serviceInfo);
            if (existingServiceInfo == null)
                this._registered.Add(serviceInfo);
            else if (existingServiceInfo.Lifetime != lifetime)
                throw new ServiceException("Attempt of adding the same service with different lifetime.");
            return this;
        }
    
        public TImplementation Resolve<TInterface, TImplementation>()
        {
            var typeInfo = this._registered.FirstOrDefault(
                r => r.Interface == typeof(TInterface) && 
                     r.Implementation == typeof(TImplementation));
            if (typeInfo == null)
                throw new ServiceException($"Type \'{typeof(TInterface).Name}\' is not registered.");
            var network = this.ResolveDependencies(typeInfo);
            return (TImplementation)this.Resolve(network.Root);
        }

        private object Resolve(ServiceGraph graph)
        {
            var parameters = new List<object>();
            foreach (var edge in graph.Edges)
                parameters.Add(this.Resolve(edge));
            var typeInfo = graph.InstanceInfo.TypeInfo;
            if (typeInfo.Lifetime == ServiceLifetime.Singleton)
            {
                var existingService = this._services.FirstOrDefault(
                    s => s.TypeInfo == typeInfo);
                if (existingService != null)
                    return existingService.Instance;
            }
            var instance = graph.InstanceInfo.Instanciate(parameters.ToArray());
            this._services.Add(new Service(instance, graph.InstanceInfo.TypeInfo));
            return instance;
        }

        private ServiceGraphNet ResolveDependencies(ServiceTypeInfo typeInfo)
        {
            var parameters = new List<object>();
            var rootGraph = new ServiceGraph(typeInfo);
            var edges = new List<ServiceGraph>();
            var network = new ServiceGraphNet(rootGraph);

            foreach (var param in rootGraph.InstanceInfo.Dependencies)
            {
                var paramTypeInfo = this._registered.FirstOrDefault(
                    r => r.Interface == param.ParameterType);
                if (paramTypeInfo == null)
                    throw new ServiceException($"Type \'{param.ParameterType.Name}\' is not registered.");
                edges.Add(this.ResolveDependency(network, paramTypeInfo));    
            }
            rootGraph.AddEdges(edges.ToArray());
            network.Validate();
            return network;
        }

        private ServiceGraph ResolveDependency(
            ServiceGraphNet network, ServiceTypeInfo typeInfo)
        {
            var graph = network.Get(typeInfo);
            var edges = new List<ServiceGraph>();
            if (graph != null)
                return graph;
            network.Add(graph = new ServiceGraph(typeInfo));
            foreach (var param in graph.InstanceInfo.Dependencies)
            {
                var paramTypeInfo = this._registered.FirstOrDefault(
                    r => r.Interface == param.ParameterType);
                if (paramTypeInfo == null)
                    throw new ServiceException($"Type \'{param.ParameterType.Name}\' is not registered.");
                edges.Add(this.ResolveDependency(network, paramTypeInfo));
            }
            graph.AddEdges(edges.ToArray());
            return graph;
        }
    }
}