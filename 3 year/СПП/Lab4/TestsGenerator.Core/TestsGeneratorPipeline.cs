using System;
using System.IO;
using System.Threading.Tasks.Dataflow;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestsGenerator.Core.Dataflow
{
    public sealed class TestsGeneratorPipeline
    {
        private readonly TransformBlock<string, GeneratorInput> _pipeline;

        public TestsGeneratorPipeline() => 
            this._pipeline = this.MakePipeline();

        public void Post(string[] files)
        {
            for (int i = 0; i < files.Length; i++)
                this._pipeline.Post(files[i]);
            this._pipeline.Complete();
            this._pipeline.Completion.Wait();
        }

        private TransformBlock<string, GeneratorInput> MakePipeline()
        {
            var options = new DataflowLinkOptions();
            options.PropagateCompletion = true;
            var loadBlock = this.LoaderFromFileBlock();
            var generateBlock = this.GenerateBlock();
            var storeBlock = this.StoreToFileBlock();
            loadBlock.LinkTo(generateBlock, options);
            generateBlock.LinkTo(storeBlock, options);
            return loadBlock;
        }

        private TransformBlock<string, GeneratorInput> LoaderFromFileBlock()
        {
            return new TransformBlock<string, GeneratorInput>(async file =>
                new GeneratorInput(file, await File.ReadAllTextAsync(file)));
        }

        private TransformBlock<GeneratorInput, GeneratorOutput> GenerateBlock()
        {
            return new TransformBlock<GeneratorInput, GeneratorOutput>(async input => 
            {
                var tree = CSharpSyntaxTree.ParseText(input.Source);
                var root = await tree.GetRootAsync();
                root = new Roslyn.TestsGenerator(
                    root as CompilationUnitSyntax).Generate();
                var path = $"{input.Path.Split(".")[0]}Tests.cs";
                Console.WriteLine($"generating tests from {input.Path}");
                return new GeneratorOutput(path, root);
            });
        }

        private ActionBlock<GeneratorOutput> StoreToFileBlock()
        {
            return new ActionBlock<GeneratorOutput>(async output =>
            {
                Console.WriteLine($"saving generated tests to {output.Path}");
                await File.WriteAllTextAsync(output.Path, output.Unit.ToFullString());
            });
        }
    }
}
