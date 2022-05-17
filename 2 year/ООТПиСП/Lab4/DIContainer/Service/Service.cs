using System;

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
    }
}