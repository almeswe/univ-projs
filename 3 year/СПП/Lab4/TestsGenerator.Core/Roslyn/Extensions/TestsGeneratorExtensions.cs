using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestsGenerator.Core.Roslyn
{
    public static class TestsGeneratorExtensions
    {
        public static string MakePrivateCase(this TestsGenerator writer, string baseName) =>
            $"_{char.ToLower(baseName[0])}{baseName.Substring(1)}";

        public static IdentifierNameSyntax MakeTestClassName(this TestsGenerator writer,
            ClassDeclarationSyntax classDeclaration)
        {
            return SyntaxFactory.IdentifierName($"{classDeclaration.Identifier}Tests")
                .WithTrailingTrivia(SyntaxFactory.EndOfLine("\r\n"));
        }

        public static IdentifierNameSyntax MakeTestNamespaceName(this TestsGenerator writer, 
            NamespaceDeclarationSyntax namespaceDeclaration)
        {
            return SyntaxFactory.IdentifierName($"{namespaceDeclaration.Name}.Tests")
                .WithTrailingTrivia(SyntaxFactory.EndOfLine("\r\n"));
        }

        public static TypeSyntax MakeTestMethodType(this TestsGenerator writer)
        {
            return SyntaxFactory.ParseTypeName("void")
                .WithTrailingTrivia(SyntaxFactory.Whitespace(" "));
        }

        public static IdentifierNameSyntax MakeTestMethodName(this TestsGenerator writer, 
            MethodDeclarationSyntax node)
        {
            var primaryName = node.Identifier.ValueText;
            var newTestName = $"{primaryName}Test";
            if (writer.MethodsFrequencyData.ContainsKey(primaryName))
            {
                if (writer.MethodsFrequencyData[primaryName] > 1)
                {
                    if (writer.MethodsFrequencyCollected.ContainsKey(primaryName))
                        writer.MethodsFrequencyCollected[primaryName]++;
                    else
                        writer.MethodsFrequencyCollected[primaryName] = 1;
                    newTestName = $"{primaryName}{writer.MethodsFrequencyCollected[primaryName]}Test";
                }
            }
            return SyntaxFactory.IdentifierName(newTestName);
        }

        public static BlockSyntax MakeTestMethodBody(this TestsGenerator writer,
            SyntaxToken methodIdentifier)
        {
            var statements = new SyntaxList<StatementSyntax>();
            statements = statements.AddRange(writer.MakeSmartStatements(methodIdentifier));
            statements = statements.Add(writer.MakeAssertFail());
            return writer.MakeBody().WithStatements(statements);
        }

        public static SyntaxList<AttributeListSyntax> MakeTestClassAttributes(this TestsGenerator writer,
            SyntaxList<AttributeListSyntax> attributeLists)
        {
            var attribute = writer.MakeAttribute("TestClass");
            var attributeList = SyntaxFactory.AttributeList(
                SyntaxFactory.SingletonSeparatedList(attribute))
                .WithLeadingTrivia(SyntaxFactory.Whitespace("\t"))
                .WithTrailingTrivia(SyntaxFactory.EndOfLine("\r\n"));
            return attributeLists.Add(attributeList);
        }

        public static SyntaxList<AttributeListSyntax> MakeMethodAttributes(this TestsGenerator writer,
            SyntaxList<AttributeListSyntax> attributeLists, string attributeValue)
        {
            var attribute = writer.MakeAttribute(attributeValue);
            var attributeList = SyntaxFactory.AttributeList(
                SyntaxFactory.SingletonSeparatedList(attribute))
                .WithLeadingTrivia(SyntaxFactory.Whitespace("\r\n\t\t"));
            return attributeLists.Add(attributeList);
        }

        public static SyntaxList<UsingDirectiveSyntax> MakeTestUsingDirectives(this TestsGenerator writer)
        {
            string[] includes = new string[]
            {
                "Moq",
                "System",
                "System.Linq",
                "System.Text",
                "System.Collections",
                "System.Collections.Generic",
                "Microsoft.VisualStudio.TestTools.UnitTesting"
            };
            var usings = new SyntaxList<UsingDirectiveSyntax>();
            foreach (var include in includes)
            {
                var usingDirectiveName = SyntaxFactory.ParseName(include)
                    .WithLeadingTrivia(SyntaxFactory.Whitespace(" "));
                var usingDirective = SyntaxFactory.UsingDirective(null, usingDirectiveName)
                    .WithTrailingTrivia(SyntaxFactory.EndOfLine("\r\n"));
                usings = usings.Add(usingDirective);
            }
            return usings;
        }

        public static ParameterListSyntax MakeTestMethodParameters(this TestsGenerator writer) =>
            SyntaxFactory.ParameterList(SyntaxFactory.SeparatedList<ParameterSyntax>())
                .WithTrailingTrivia(SyntaxFactory.EndOfLine("\r\n"));

        public static BlockSyntax MakeBody(this TestsGenerator writer)
        {
            return SyntaxFactory.Block()
                .WithLeadingTrivia(SyntaxFactory.Whitespace("\t\t"))
                .WithTrailingTrivia(SyntaxFactory.EndOfLine("\r\n"))
                .WithOpenBraceToken(SyntaxFactory.Token(SyntaxKind.OpenBraceToken)
                    .WithLeadingTrivia(SyntaxFactory.Whitespace("\t\t"))
                    .WithTrailingTrivia(SyntaxFactory.EndOfLine("\r\n")))
                .WithCloseBraceToken(SyntaxFactory.Token(SyntaxKind.CloseBraceToken)
                    .WithLeadingTrivia(SyntaxFactory.Whitespace("\t\t"))
                    .WithTrailingTrivia(SyntaxFactory.EndOfLine("\r\n")));
        }

        public static SyntaxList<AttributeListSyntax> MakeTestMethodAttributes(this TestsGenerator writer,
            SyntaxList<AttributeListSyntax> attributeLists)
        {
            return writer.MakeMethodAttributes(attributeLists, "TestMethod");
        }

        public static SyntaxList<AttributeListSyntax> MakeInitMethodAttributes(this TestsGenerator writer,
            SyntaxList<AttributeListSyntax> attributeLists)
        {
            return writer.MakeMethodAttributes(attributeLists, "TestInitialize");
        }

        public static AttributeSyntax MakeAttribute(this TestsGenerator writer, string attributeValue)
        {
            return SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(attributeValue));
        }

        public static ExpressionSyntax MakeLiteralExpression(this TestsGenerator writer, TypeSyntax type)
        {
            switch (type.ToString())
            {
                case "bool":
                case "Boolean":
                    return SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression,
                        SyntaxFactory.Token(SyntaxKind.TrueKeyword));
                case "char":
                case "Char":
                    return SyntaxFactory.LiteralExpression(SyntaxKind.CharacterLiteralExpression,
                        SyntaxFactory.Literal('\0'));
                case "byte":
                case "Byte":
                case "sbyte":
                case "Sbyte":
                case "Int16":
                case "short":
                case "UInt16":
                case "ushort":
                case "Int32":
                case "int":
                case "UInt32":
                case "uint":
                case "Int64":
                case "long":
                case "UInt64":
                case "ulong":
                    return SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression,
                        SyntaxFactory.Literal(0));
                case "float":
                case "Single":
                case "double":
                case "Double":
                    return SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression,
                        SyntaxFactory.Literal(0.0d));
                case "string":
                case "String":
                    return SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression,
                        SyntaxFactory.Literal(""));
                default:
                    return SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression,
                        SyntaxFactory.Token(SyntaxKind.NullKeyword));
            }
        }

        public static StatementSyntax MakeNewExpressionStatement(this TestsGenerator writer,
            SyntaxToken storage, TypeSyntax typeSyntax, ArgumentListSyntax args)
        {
            args = args.WithoutLeadingTrivia();
            typeSyntax = typeSyntax.WithLeadingTrivia(SyntaxFactory.Whitespace(" "));
            var rvalue = SyntaxFactory.ObjectCreationExpression(typeSyntax, args, null)
                .WithLeadingTrivia(SyntaxFactory.Whitespace(" "));
            var lvalue = SyntaxFactory.IdentifierName(storage)
                    .WithTrailingTrivia(SyntaxFactory.Whitespace(" "));
            var statement = SyntaxFactory.ExpressionStatement(
                SyntaxFactory.AssignmentExpression(
                    SyntaxKind.SimpleAssignmentExpression, lvalue, rvalue))
                .WithLeadingTrivia(SyntaxFactory.Whitespace("\t\t\t"))
                .WithTrailingTrivia(SyntaxFactory.EndOfLine("\r\n"));
            return statement;
        }

        public static TypeSyntax MakeMockGenericName(this TestsGenerator writer, TypeSyntax typeSyntax)
        {
            return SyntaxFactory.GenericName(
                SyntaxFactory.Identifier("Mock"),
                SyntaxFactory.TypeArgumentList(
                    SyntaxFactory.SingletonSeparatedList(typeSyntax)));
        }
    }
}