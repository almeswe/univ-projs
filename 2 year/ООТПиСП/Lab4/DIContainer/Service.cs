using System;
using System.Linq;
using System.Reflection;

namespace DIContainer
{
    public sealed class Service
    {
        public Guid Descriptor { get; private set; }
        public object Instance { get; private set; }
        public ServiceTypeInfo TypeInfo { get; private set; }

        public Service(object instance, ServiceTypeInfo info)
        {
            this.TypeInfo = info;
            this.Instance = instance;
            this.Descriptor = Guid.NewGuid();
        }

        /*public object Instanciate(ConstructorInfo constructor, object[] parameters)
        {
            if (this.IsInstanciated && this.Lifetime == ServiceLifetime.Singleton)
                return this._instances[0];
            if (this.Type.GetConstructors().FirstOrDefault(c => c == constructor) != null)
                throw new ServiceException("Service type does not contains specified constructor.");
            try
            {
                var instance = constructor.Invoke(parameters);
                this._instances.Add(instance);
                this.IsInstanciated = true;
                return instance;
            }
            catch (Exception e)
            {
                throw new ServiceException($"Cannot instanciate service, {e.Message}");
            }
        }*/
    }
}