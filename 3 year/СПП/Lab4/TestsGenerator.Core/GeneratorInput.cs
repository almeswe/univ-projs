namespace TestsGenerator.Core.Dataflow
{
    public sealed class GeneratorInput
    {
        public string Path { get; private set; }
        public string Source { get; private set; }

        public GeneratorInput(string path, string source)
        {
            this.Path = path;
            this.Source = source;
        }
    }
}
