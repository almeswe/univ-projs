using System;
using System.IO;
using System.Text;
using System.Reflection;

using Tracer.Serialization.Abstractions;

namespace Tracer.Core.Example
{
    public static class SerializationLoader
    {
        private static string _asmPrefix = "Tracer.Serialization.";
        private static Assembly _asm = Assembly.GetExecutingAssembly();

        public static ITraceResultSerializer Load(SerializationType type)
        {
            var path = Path.GetDirectoryName(_asm.Location);
            var asmName = $"{_asmPrefix}{GetSerializationTypeString(type)}";
            var plugIn = Assembly.LoadFile(Path.Combine(path, $"{asmName}.dll"));
            var serializerType = plugIn.GetType($"{asmName}.Serializer");
            return Activator.CreateInstance(serializerType) as ITraceResultSerializer;
        }

        private static string GetSerializationTypeString(SerializationType type)
        {
            var typeString = type.ToString();
            var builder = new StringBuilder();
            builder.Append(typeString[0]);
            for (int i = 1; i < typeString.Length; i++)
                builder.Append(char.ToLower(typeString[i]));
            return builder.ToString();
        }
    }
}
