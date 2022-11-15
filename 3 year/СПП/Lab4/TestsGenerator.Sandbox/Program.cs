using System;

using TestsGenerator.Core.Dataflow;

namespace TestsGenerator.Sandbox
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var generator = new TestsGeneratorPipeline();
            generator.Post(new string[]
            {
                "tests/class1.cs",
                "tests/class2.cs",
                "tests/class3.cs",
                "tests/class4.cs",
            });
            Console.ReadKey();
        }
    }
}