using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestsGenerator.Core.Dataflow
{
    public sealed class GeneratorOutput
    {
        public string Path { get; private set; }
        public CompilationUnitSyntax Unit { get; private set; }

        public GeneratorOutput(string path, SyntaxNode unit)
        {
            this.Path = path;
            this.Unit = unit as CompilationUnitSyntax;
        }
    }
}
