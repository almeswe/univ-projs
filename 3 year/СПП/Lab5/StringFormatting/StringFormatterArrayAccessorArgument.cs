using System;
using System.Linq;
using System.Reflection;
using System.Collections;

namespace StringFormatting
{
    public sealed class StringFormatterArrayAccessorArgument
    {
        public int[] Indexes { get; private set; }
        public object Value { get; private set; }
        public Type ValueType { get; private set; }
        private readonly MethodInfo[] _cachedMethods;

        public StringFormatterArrayAccessorArgument(
            StringFormatterArgument argument, int[] indexes)
        {
            this.Value = argument.Value;
            this.Indexes = indexes;
            this.ValueType = argument.ValueType;
            this._cachedMethods = typeof(IList).GetMethods();
        }

        private bool IsCollection(Type type) =>
            type.GetInterfaces().Contains(typeof(IList));

        private MethodInfo GetCollectionMethod(string name)
        {
            foreach (var method in this._cachedMethods)
                if (method.Name == name)
                    return method;
            return null;
        }

        public override string ToString()
        {
            object value = this.Value;
            for (int i = 0; i < this.Indexes.Length; i++)
            {
                if (!this.IsCollection(value.GetType()))
                    throw new InvalidContextFormatterException();
                value = this.GetCollectionMethod("get_Item").Invoke(
                    value, new object[] { this.Indexes[i] });
            }
            return value.ToString();
        }
    }
}
