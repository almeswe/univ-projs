using System.Collections.Generic;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestsGenerator.Core.Roslyn
{
    public sealed class TestsGenerator : CSharpSyntaxRewriter
    {
        public CompilationUnitSyntax Root { get; private set; }
        public Dictionary<string, int> MethodsFrequencyData { get; private set; }
        public Dictionary<string, int> MethodsFrequencyCollected { get; private set; }
        public Dictionary<string, MethodDeclarationSyntax> MethodsMappings { get; private set; }

        public List<ConstructorDeclarationSyntax> ConstructorsData { get; private set; }

        public TestsGenerator(CompilationUnitSyntax root)
        {
            var statWalker = new TestsStatistics();
            statWalker.Visit(root);
            this.Root = root;
            this.ConstructorsData = statWalker.Constructors;
            this.MethodsFrequencyData = statWalker.MethodsFrequencyData;
            this.MethodsFrequencyCollected = new Dictionary<string, int>();
            this.MethodsMappings = new Dictionary<string, MethodDeclarationSyntax>();
        }

        public CompilationUnitSyntax Generate() =>
            this.Visit(this.Root) as CompilationUnitSyntax;

        public override SyntaxNode VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            return base.VisitNamespaceDeclaration(node
                .WithName(this.MakeTestNamespaceName(node)));
        }

        public override SyntaxNode VisitCompilationUnit(CompilationUnitSyntax node)
        {
            return base.VisitCompilationUnit(node
                .WithUsings(this.MakeTestUsingDirectives()));
        }

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var classDeclaration = base.VisitClassDeclaration(node
                .WithIdentifier(this.MakeTestClassName(node).Identifier)
                .WithAttributeLists(this.MakeTestClassAttributes(node.AttributeLists)));
            return this.MakeSetUp(classDeclaration as ClassDeclarationSyntax);
        }

        public override SyntaxNode VisitFieldDeclaration(FieldDeclarationSyntax node) => null;
        public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node) => null;
        public override SyntaxNode VisitDestructorDeclaration(DestructorDeclarationSyntax node) => null;
        public override SyntaxNode VisitConstructorDeclaration(ConstructorDeclarationSyntax node) => null;

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (node.Modifiers.Any(SyntaxKind.PublicKeyword))
            {
                var name = this.MakeTestMethodName(node);
                this.MethodsMappings[name.Identifier.ValueText] = node;
                var methodDeclaration = node
                    .WithBody(this.MakeTestMethodBody(name.Identifier))
                    .WithIdentifier(name.Identifier)
                    .WithReturnType(this.MakeTestMethodType())
                    .WithAttributeLists(this.MakeTestMethodAttributes(node.AttributeLists));
                this.Root = this.Root.ReplaceNode(node, methodDeclaration);
                return base.VisitMethodDeclaration(methodDeclaration);
            }
            return null;
        }

        public override SyntaxNode VisitParameterList(ParameterListSyntax node) =>
            this.MakeTestMethodParameters();
    }
}