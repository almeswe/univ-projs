using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace TestsGenerator.Core.Roslyn
{
    public static class TestsGeneratorSmartExtensions
    {
        public static SyntaxList<ParameterSyntax> MakeDependencies(this TestsGenerator writer)
        {
            var dependencies = new SyntaxList<ParameterSyntax>();
            foreach (var constructor in writer.ConstructorsData)
            {
                var interfaceDependencyOnly = true;
                foreach (var parameter in constructor.ParameterList.Parameters)
                    if (parameter.Type.ToString()[0] != 'I')
                        interfaceDependencyOnly = false;
                if (interfaceDependencyOnly)
                {
                    if (dependencies.Count >= constructor.ParameterList.Parameters.Count)
                        continue;
                    dependencies = new SyntaxList<ParameterSyntax>();
                    foreach (var parameter in constructor.ParameterList.Parameters)
                        dependencies = dependencies.Add(parameter);
                }
            }
            return dependencies;
        }

        public static MemberDeclarationSyntax MakeSetUpInstanceMember(this TestsGenerator writer,
            ClassDeclarationSyntax classDeclaration)
        {
            var name = classDeclaration.Identifier.ValueText;
            var type = name.Split("Tests")[0];
            var identifier = SyntaxFactory.Identifier(writer.MakePrivateCase(name));
            var instanceMember = SyntaxFactory.FieldDeclaration(
                SyntaxFactory.VariableDeclaration(
                    SyntaxFactory.ParseTypeName(type)
                        .WithTrailingTrivia(SyntaxFactory.Whitespace(" ")),
                    SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.VariableDeclarator(identifier, null, null))))
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword)
                    .WithTrailingTrivia(SyntaxFactory.Whitespace(" "))
                    .WithLeadingTrivia(SyntaxFactory.Whitespace("\t\t")));
            instanceMember = instanceMember.WithTrailingTrivia(
                SyntaxFactory.EndOfLine("\r\n"));
            return instanceMember;
        }

        public static SyntaxList<MemberDeclarationSyntax> MakeSetUpMembers(this TestsGenerator writer,
            SyntaxList<ParameterSyntax> dependencies)
        {
            var setUpFields = new SyntaxList<MemberDeclarationSyntax>();
            foreach (var dependency in dependencies)
            {
                var identifier = SyntaxFactory.Identifier($"_{dependency.Identifier.ValueText}");
                var setUpField = SyntaxFactory.FieldDeclaration(
                    SyntaxFactory.VariableDeclaration(writer.MakeMockGenericName(dependency.Type),
                        SyntaxFactory.SingletonSeparatedList(
                            SyntaxFactory.VariableDeclarator(identifier, null, null))))
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword)
                        .WithTrailingTrivia(SyntaxFactory.Whitespace(" "))
                        .WithLeadingTrivia(SyntaxFactory.Whitespace("\t\t")));
                setUpField = setUpField.WithTrailingTrivia(
                    SyntaxFactory.EndOfLine("\r\n"));
                setUpFields = setUpFields.Add(setUpField);
            }
            return setUpFields;
        }

        public static StatementSyntax MakeSetUpInstanceStatement(this TestsGenerator writer,
            ClassDeclarationSyntax classDeclaration, SyntaxList<MemberDeclarationSyntax> members)
        {
            var arguments = SyntaxFactory.ArgumentList();
            foreach (var member in members)
            {
                var variable = (member as FieldDeclarationSyntax).Declaration.Variables.First();
                arguments = arguments.AddArguments(SyntaxFactory.Argument(
                    SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.IdentifierName(variable.Identifier),
                        SyntaxFactory.IdentifierName("Object"))));
            }
            var identifier = SyntaxFactory.Identifier(
                writer.MakePrivateCase(classDeclaration.Identifier.ValueText));
            var typeSyntax = SyntaxFactory.IdentifierName(
                classDeclaration.Identifier.ValueText.Split("Tests")[0])
                .WithoutTrailingTrivia();
            return writer.MakeNewExpressionStatement(identifier, typeSyntax, arguments);
        }

        public static BlockSyntax MakeSetUpMethodBody(this TestsGenerator writer,
            ClassDeclarationSyntax classDeclaration, SyntaxList<MemberDeclarationSyntax> members)
        {
            var statements = new SyntaxList<StatementSyntax>();
            foreach (var member in members)
            {
                var type = (member as FieldDeclarationSyntax).Declaration.Type;
                var variable = (member as FieldDeclarationSyntax).Declaration.Variables.First();
                var statement = writer.MakeNewExpressionStatement(
                    variable.Identifier, type, SyntaxFactory.ArgumentList());
                statements = statements.Add(statement);
            }
            var instanceStatement = writer.MakeSetUpInstanceStatement(classDeclaration, members);
            statements = statements.Add(instanceStatement);
            return writer.MakeBody()
                .WithStatements(statements)
                .WithOpenBraceToken(SyntaxFactory.Token(SyntaxKind.OpenBraceToken)
                    .WithLeadingTrivia(SyntaxFactory.Whitespace("\r\n\t\t"))
                    .WithTrailingTrivia(SyntaxFactory.EndOfLine("\r\n")));
        }

        public static MemberDeclarationSyntax MakeSetUpMethod(this TestsGenerator writer,
            ClassDeclarationSyntax classDeclaration, SyntaxList<MemberDeclarationSyntax> dependencies)
        {
            var attributes = writer.MakeInitMethodAttributes(
                new SyntaxList<AttributeListSyntax>());
            var type = SyntaxFactory.IdentifierName("void")
                .WithTrailingTrivia(SyntaxFactory.Whitespace(" "));
            var setUpMethod = SyntaxFactory.MethodDeclaration(type, "SetUp")
                .WithAttributeLists(attributes)
                .WithBody(writer.MakeSetUpMethodBody(classDeclaration, dependencies));
            setUpMethod = setUpMethod.AddModifiers(
                SyntaxFactory.Token(SyntaxKind.PublicKeyword)
                    .WithLeadingTrivia(SyntaxFactory.EndOfLine("\r\n\t\t"))
                    .WithTrailingTrivia(SyntaxFactory.Whitespace(" ")))
                .WithTrailingTrivia(SyntaxFactory.EndOfLine("\r\n"));
            return setUpMethod;
        }

        public static ClassDeclarationSyntax MakeSetUp(this TestsGenerator writer,
            ClassDeclarationSyntax classDeclaration)
        {
            var dependencies = writer.MakeDependencies();
            var members = new SyntaxList<MemberDeclarationSyntax>();
            var fieldMembers = writer.MakeSetUpMembers(dependencies);
            var method = writer.MakeSetUpMethod(classDeclaration, fieldMembers);
            members = members.Add(writer.MakeSetUpInstanceMember(classDeclaration));
            members = members.AddRange(fieldMembers);
            members = members.Add(method);
            members = members.AddRange(classDeclaration.Members);
            return classDeclaration.WithMembers(members);
        }

        public static SyntaxList<StatementSyntax> MakeArrangeTestStatements(this TestsGenerator writer,
            MethodDeclarationSyntax methodDeclaration)
        {
            var statements = new SyntaxList<StatementSyntax>();
            foreach (var parameter in methodDeclaration.ParameterList.Parameters)
            {
                var literal = writer.MakeLiteralExpression(parameter.Type)
                    .WithLeadingTrivia(SyntaxFactory.Whitespace(" "));
                var identifier = SyntaxFactory.Identifier($"{parameter.Identifier}_t")
                    .WithTrailingTrivia(SyntaxFactory.Whitespace(" "));
                var variableDeclaration = SyntaxFactory.VariableDeclaration(
                    parameter.Type, SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.VariableDeclarator(identifier, null, 
                            SyntaxFactory.EqualsValueClause(literal))));
                var statement = SyntaxFactory.LocalDeclarationStatement(variableDeclaration)
                    .WithLeadingTrivia(SyntaxFactory.Whitespace("\t\t\t"))
                    .WithTrailingTrivia(SyntaxFactory.EndOfLine("\r\n"));
                statements = statements.Add(statement);
            }
            return statements;
        }

        public static StatementSyntax MakeActTestStatements(this TestsGenerator writer, 
            MethodDeclarationSyntax methodDeclaration, SyntaxList<StatementSyntax> localVariables)
        {
            var variables = new SyntaxList<VariableDeclaratorSyntax>();
            foreach (var localVariable in localVariables)
            {
                var local = localVariable as LocalDeclarationStatementSyntax;
                variables = variables.Add(local.Declaration.Variables.First());
            }
            var invokationExpressionArguments = SyntaxFactory.ArgumentList();
            foreach (var variable in variables)
                invokationExpressionArguments = invokationExpressionArguments.AddArguments(
                    SyntaxFactory.Argument(SyntaxFactory.IdentifierName(variable.Identifier)
                        .WithoutTrailingTrivia()));
            var classMember = methodDeclaration.Parent as ClassDeclarationSyntax;
            var classMemberName = writer.MakePrivateCase(classMember.Identifier.ValueText);
            var invokationExpression = SyntaxFactory.InvocationExpression(
                SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                    SyntaxFactory.IdentifierName(classMemberName), 
                    SyntaxFactory.IdentifierName(methodDeclaration.Identifier)),
                        invokationExpressionArguments);
            if (methodDeclaration.ReturnType.ToString() == "void")
                return SyntaxFactory.ExpressionStatement(invokationExpression)
                    .WithLeadingTrivia(SyntaxFactory.Whitespace("\t\t\t"))
                    .WithTrailingTrivia(SyntaxFactory.EndOfLine("\r\n"));
            var declarationExpression = SyntaxFactory.VariableDeclaration(
                methodDeclaration.ReturnType, SyntaxFactory.SingletonSeparatedList(
                    SyntaxFactory.VariableDeclarator(
                        SyntaxFactory.Identifier("actual"), null, 
                            SyntaxFactory.EqualsValueClause(invokationExpression))));
            return SyntaxFactory.LocalDeclarationStatement(declarationExpression)
                .WithLeadingTrivia(SyntaxFactory.Whitespace("\t\t\t"))
                .WithTrailingTrivia(SyntaxFactory.EndOfLine("\r\n"));
        }

        public static SyntaxList<StatementSyntax> MakeAssertTestStatements(this TestsGenerator writer,
            MethodDeclarationSyntax methodDeclaration)
        {
            var statements = new SyntaxList<StatementSyntax>();
            if (methodDeclaration.ReturnType.ToString() == "void")
                return statements;
            var literal = writer.MakeLiteralExpression(methodDeclaration.ReturnType);
            var declarationExpression = SyntaxFactory.VariableDeclaration(
                methodDeclaration.ReturnType, SyntaxFactory.SingletonSeparatedList(
                    SyntaxFactory.VariableDeclarator(
                        SyntaxFactory.Identifier("expected"), null,
                            SyntaxFactory.EqualsValueClause(literal))));
            StatementSyntax statement = SyntaxFactory.LocalDeclarationStatement(declarationExpression)
                .WithLeadingTrivia(SyntaxFactory.Whitespace("\t\t\t"))
                .WithTrailingTrivia(SyntaxFactory.EndOfLine("\r\n"));
            statements = statements.Add(statement);
            statement = writer.MakeAssertAreEqual(
                SyntaxFactory.IdentifierName("expected"),
                SyntaxFactory.IdentifierName("actual"));
            statements = statements.Add(statement);
            return statements;
        }

        public static SyntaxList<StatementSyntax> MakeSmartStatements(this TestsGenerator writer,
            SyntaxToken methodIdentifier)
        {
            var statements = new SyntaxList<StatementSyntax>();
            var methodDeclaration = writer.MethodsMappings[methodIdentifier.ValueText];
            statements = statements.AddRange(writer.MakeArrangeTestStatements(methodDeclaration));
            statements = statements.Add(writer.MakeActTestStatements(methodDeclaration, statements));
            statements = statements.AddRange(writer.MakeAssertTestStatements(methodDeclaration));
            return statements;
        }
    }
}