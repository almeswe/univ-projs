using System;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

using Serialization.XML;

namespace Serialization.XMLSerialization
{
    public static class XMLSerializer
    {
        public static XMLChildObject Serialize<T>(T item)
        {
            var type = item.GetType();
            var child = new XMLChildObject(type.Name);
            var rootChildChilds = new List<XMLChildObject>(); 
            foreach (var field in GetTypeFields(type))
            {
                rootChildChilds.Add(IsPrimitive(field.FieldType) ?
                    SerializePrimitive(item, field) :
                    SerializeNonPrimitive(item, field));
            }
            child.Value = new XMLValueChilds(rootChildChilds.ToArray());
            return child;
        }

        private static XMLChildObject SerializePrimitive(object obj, FieldInfo field)
        {
            var child = new XMLChildObject(field.Name);
            if (!IsPrimitive(field.FieldType))
                throw new XMLSerializerException("Type must be primitive");
            if (field.FieldType == typeof(string))
                if (field.GetValue(obj) == null)
                    return child;
            child.Value = new XMLValueImmediate(field.GetValue(obj).ToString());
            return child;
        }

        private static XMLChildObject SerializeNonPrimitive(object obj, FieldInfo field)
        {
            var child = new XMLChildObject(field.Name);
            var childs = new List<XMLChildObject>();
            if (IsPrimitive(field.FieldType))
                throw new XMLSerializerException("Type must be non-primitive");
            if (field.GetValue(obj) == null)
                return child;
            foreach (var typeField in GetTypeFields(field.FieldType))
            {
                var item = field.GetValue(obj);
                childs.Add(IsPrimitive(typeField.FieldType) ? 
                    SerializePrimitive(item, typeField) : 
                    SerializeNonPrimitive(item, typeField));
            }
            child.Value = new XMLValueChilds(childs.ToArray());
            return child;
        }

        public static FieldInfo[] GetTypeFields(Type type)
        {
            var fields = new List<FieldInfo>();
            for (var baseType = type; baseType != typeof(object); 
                    baseType = baseType.BaseType)
                fields.AddRange(baseType.GetFields(
                    (BindingFlags)int.MaxValue));
            foreach (var interfaceType in type.GetInterfaces())
                fields.AddRange(interfaceType.GetFields(
                    (BindingFlags)int.MaxValue));
            return fields.ToArray();
        }

        public static bool IsPrimitive(Type type) =>
            type.IsPrimitive || type == typeof(string);
    }

    public static class XMLDeserializer
    {
        public static object Deserialize(Type type, XMLChildObject child)
        {
            return XMLSerializer.IsPrimitive(type) ?
                DeserializePrimitive(type, child) :
                DeserializeNonPrimitive(type, child);
        }

        private static object DeserializeNonPrimitive(Type type, XMLChildObject child)
        {
            var constructor = type.GetConstructor(new Type[] { });
            if (constructor == null)
                throw new XMLSerializerException(
                    "Cannot find empty contructor of deserializable type");
            var deserialized = constructor.Invoke(new object[] { });
            if (child.Value == null)
                return deserialized;
            if (child.Value.GetType() == typeof(XMLValueImmediate))
                throw new XMLSerializerException(
                    "Cannot deserialize non-primitive object from immediate XML value.");
            foreach (var typeField in XMLSerializer.GetTypeFields(deserialized.GetType()))
                foreach (var dumped in (child.Value as XMLValueChilds).Childs)
                    if (dumped.Tag == typeField.Name)
                        typeField.SetValue(deserialized, XMLSerializer.IsPrimitive(typeField.FieldType) ?
                            DeserializePrimitive(typeField.FieldType, dumped) :
                            DeserializeNonPrimitive(typeField.FieldType, dumped));
            return deserialized;
        }

        private static object DeserializePrimitive(Type type, XMLChildObject child)
        {
            if (child.Value == null)
                return null;
            if (child.Value.GetType() != typeof(XMLValueImmediate))
                throw new XMLSerializerException("Cannot deserialize non-primitive type.");
            if (type == typeof(string))
                return (child.Value as XMLValueImmediate).Value;
            var convert = type.GetMethod("Parse", new Type[] { typeof(string) });
            if (convert == null)
                throw new XMLSerializerException("Cannot find appropriate method for deserializing.");
            return convert.Invoke(null, new object[] { 
                (child.Value as XMLValueImmediate).Value });
        }
    }
}