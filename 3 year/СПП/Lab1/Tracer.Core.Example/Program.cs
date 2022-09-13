using System;
using System.IO;

namespace Tracer.Core.Example
{
    public sealed class Program
    {
        private static Tracer _tracer = new Tracer();

        private static void Main(string[] args)
        {
            new TestClass(_tracer).M0();
            var result = _tracer.GetTraceResult();
            var serializer = SerializationLoader.Load(SerializationType.YAML);
            using (var fs = File.Open("SerializedData.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                serializer.Serialize(result, fs);
            }
            result.Tree.PrintTree();
            Console.ReadLine();
        }
    }
}
