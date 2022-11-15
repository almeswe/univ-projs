using System.Linq;
using System.Collections.Generic;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestsGenerator.Core.Roslyn
{
    public sealed class TestsStatistics : CSharpSyntaxWalker
    {
        public List<ConstructorDeclarationSyntax> Constructors { get; private set; }
        public Dictionary<string, int> MethodsFrequencyData { get; private set; }

        public TestsStatistics()
        {
            this.Constructors = new List<ConstructorDeclarationSyntax>();
            this.MethodsFrequencyData = new Dictionary<string, int>();
        }

        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            this.Constructors.Add(node);
            base.VisitConstructorDeclaration(node);
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (node.Modifiers.Any(SyntaxKind.PublicKeyword))
            {
                var methodName = node.Identifier.ValueText;
                if (this.MethodsFrequencyData.ContainsKey(methodName))
                    this.MethodsFrequencyData[methodName]++;
                else
                {
                    this.MethodsFrequencyData[methodName] = 1;
                }
            }
            base.VisitMethodDeclaration(node);
        }
    }
}