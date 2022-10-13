using System;
using System.Reflection;

namespace StringFormatting
{
    public class StringFormatterArgument
    {
        public object Value { get; protected set; }
        public Type ValueType { get; protected set; }
        public string UniqueName { get; protected set; }

        public StringFormatterArgument(object fromObj, FieldInfo fromField)
        {
            this.UniqueName = fromField.Name;
            this.Value = fromField.GetValue(fromObj);
            this.ValueType = fromField.FieldType;
        }

        public StringFormatterArgument(object fromObj, PropertyInfo fromProperty)
        {
            this.UniqueName = fromProperty.Name;
            this.Value = fromProperty.GetValue(fromObj);
            this.ValueType = fromProperty.PropertyType;
        }

        public override string ToString() =>
            this.Value.ToString();
    }
}
