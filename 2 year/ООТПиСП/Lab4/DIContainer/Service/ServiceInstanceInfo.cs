using System.Linq;
using System.Reflection;

namespace DIContainer
{
    public sealed class ServiceInstanceInfo
    {
        public ServiceTypeInfo TypeInfo { get; private set; }
        public ConstructorInfo Constructor { get; private set; }
        public ParameterInfo[] Dependencies { get => this.Constructor.GetParameters(); }

        public ServiceInstanceInfo(ServiceTypeInfo typeInfo)
        {
            this.TypeInfo = typeInfo;
            this.GetConstructorInfo();
        }

        private void GetConstructorInfo()
        {
            var type = this.TypeInfo.Implementation;
            var constructors = type.GetConstructors();
            if (constructors.Length <= 0)
                throw new ServiceException($"There are no constructors listed for \'{type.Name}\'.");
            var appropriate = constructors.FirstOrDefault(
                c => c.IsPublic);
            if (appropriate == null)
                throw new ServiceException($"There are no available constructors found for \'{type.Name}\'.");
            this.Constructor = constructors[0];
        }

        public object Instanciate(object[] parameters) =>
            this.Constructor.Invoke(parameters);
    }
}