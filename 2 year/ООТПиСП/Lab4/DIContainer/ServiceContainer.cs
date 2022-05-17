using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;

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
                implementationType, ServiceLifetime.Transient);
            var existingServiceInfo = this._registered.FirstOrDefault(
                r => r == serviceInfo);
            if (existingServiceInfo == null)
                this._registered.Add(serviceInfo);
            else if (existingServiceInfo.Lifetime != lifetime)
                throw new ServiceException("Attempt of adding the same service with different lifetime.");
            return this;
        }
    
        public TImplementation Resolve<TImplementation>() =>
            (TImplementation)this.Resolve(typeof(TImplementation));

        private object Resolve(Type implementation)
        {
            var parameters = new List<object>();
            var constructor = this.ResolveConstructor(implementation);
            if (constructor == null)
                throw new ServiceException($"Cannot resolve appropriate constructor" +
                    $"for type \'{implementation.Name}\'.");
            foreach (var param in constructor.GetParameters())
            {
                var type = param.ParameterType;
                if (!this.ResolveInterface(type))
                    throw new ServiceException($"Type \'{type.Name}\' is not registered.");
                var serviceInfo = this._registered.FirstOrDefault(
                    r => r.Interface == type);
                parameters.Add(this.Resolve(serviceInfo.Implementation));
            }
            return constructor.Invoke(parameters.ToArray());
        }

        private bool ResolveInterface(Type type)
        {
            var info = this._registered.FirstOrDefault(
                r => r.Interface == type);
            return info != null;
        }

        private bool ResolveImplementation(Type type)
        {
            var info = this._registered.FirstOrDefault(
                r => r.Implementation == type);
            return info != null;
        }

        private ConstructorInfo ResolveConstructor(Type type)
        {
            var constructors = type.GetConstructors();
            if (constructors.Length <= 0)
                return null;
            var appropriate = constructors.FirstOrDefault(
                c => c.IsPublic);
            if (appropriate == null)
                return null;
            return constructors[0];
        }
    }
}
