using System;

namespace DIContainer
{
    public sealed class ServiceTypeInfo
    {
        public Type Interface { get; private set; }
        public Type Implementation { get; private set; }
        public ServiceLifetime Lifetime { get; private set; }

        public ServiceTypeInfo(Type @interface,
            Type implementation, ServiceLifetime lifetime)
        {
            this.Lifetime = lifetime;
            this.Interface = @interface;
            this.Implementation = implementation;
        }

        public static bool operator ==(ServiceTypeInfo a, ServiceTypeInfo b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (ReferenceEquals(a, null))
                return false;
            if (ReferenceEquals(b, null))
                return false;
            return a.Interface == b.Interface && 
                a.Implementation == a.Implementation;
        }

        public static bool operator !=(ServiceTypeInfo a, ServiceTypeInfo b)
        {
            if (ReferenceEquals(a, b))
                return false;
            if (ReferenceEquals(a, null))
                return true;
            if (ReferenceEquals(b, null))
                return true;
            return a.Interface != b.Interface ||
                a.Implementation != b.Implementation;
        }
    }
}