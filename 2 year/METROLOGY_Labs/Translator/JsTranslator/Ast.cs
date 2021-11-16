using System;
using System.Collections.Generic;
using System.Linq;

namespace Translator.JsTranslator.Syntax
{
    public abstract class SyntaxTreeNode
    {
        public SyntaxTreeNode Parent { get; protected set; }
        public List<SyntaxTreeNode> Childs { get; protected set; }

        public void AddNode(SyntaxTreeNode node)
        {
            if (this.Childs == null)
                this.Childs = new List<SyntaxTreeNode>();

            if (node == null)
                return;

            node.Parent = this;
            this.Childs.Add(node);
        }

        public void AddNodes(params SyntaxTreeNode[] nodes)
        {
            if (this.Childs == null)
                this.Childs = new List<SyntaxTreeNode>();
            foreach (SyntaxTreeNode node in nodes)
                this.AddNode(node);
        }

        public SyntaxTreeNode GetParentByType(Type type)
        {
            for (var Parent = this.Parent; Parent != null; Parent = Parent.Parent)
                if (Parent.GetType().ToString() == type.ToString())
                    return Parent;
            return null;
        }

        public SyntaxTreeNode[] GetChildsByType(Type type, bool recursive = false, bool checkThisOnce = true)
        {
            var Childs = new List<SyntaxTreeNode>();
            if (checkThisOnce)
                if (this.GetType().ToString() == type.ToString())
                    Childs.Add(this);
            for (int i = 0; i < this.Childs.Count; i++)
            {
                if (this.Childs[i].GetType().ToString() == type.ToString())
                    Childs.Add(this.Childs[i]);
                if (recursive)
                    Childs.AddRange(this.Childs[i].GetChildsByType(type, true, false));
            }
            return Childs.ToArray();
        }

        public override string ToString() =>
            throw new NotImplementedException();
    }

    public sealed class AstRoot : SyntaxTreeNode
    {
        public List<Statement> Statements { get; private set; }

        public AstRoot(List<Statement> statements) =>
            this.AddNodes((this.Statements = statements).ToArray());

        public static void Print(SyntaxTreeNode node, string indent = "", bool isRoot = false)
        {
            if (isRoot)
                Console.Write($"{indent}└──{node}\n");

            indent += "   ";
            foreach (var child in node.Childs)
            {
                if (child != null)
                {
                    Console.Write($"{indent} {(child == node.Childs.Last() ? "└──" : "├──")}{child}\n");
                    Print(child, indent + (child.Childs.Count >= 1 && child == node.Childs.Last()
                        ? string.Empty : " │"));
                }
            }
        }

        public override string ToString() =>
            $"ast-root";
    }

    public abstract class Statement : SyntaxTreeNode {}
    
    public abstract class Expression : SyntaxTreeNode {}

    public sealed class Identifier : Expression
    {
        public string Value { get; private set; }

        public Identifier(string value)
        {
            this.Value = value;
            this.Childs = new List<SyntaxTreeNode>();
        }

        public override string ToString() =>
            $"Identifier: {this.Value}";
    }

    public sealed class Constant : Expression
    {
        public enum ConstantKind
        {
            RealConstant,
            StringConstant,
            IntegerConstant,
            LiteralConstant
        }

        public string Value { get; private set; }
        public ConstantKind Kind { get; private set; }

        public Constant(string value, ConstantKind kind)
        {
            this.Kind = kind;
            this.Value = value;
            this.Childs = new List<SyntaxTreeNode>();
        }

        public override string ToString() =>
            $"{this.Kind}: {this.Value}";
    }

    public sealed class FunctionCall : Expression
    {
        public string Name { get; private set; }
        public List<Expression> Arguments { get; private set; }
    
        public FunctionCall(string name, List<Expression> arguments)
        {
            this.Name = name;
            this.AddNodes((this.Arguments = arguments).ToArray());
        }

        public override string ToString() =>
            $"func-call: {this.Name} (args: {this.Arguments.Count})";
    }

    public sealed class UnaryExpression : Expression
    {
        public enum UnaryExpressionKind : int
        {
            UnaryPlus,
            UnaryMinus,
            UnaryLgNot,
            UnaryBwNot
        }

        public Expression Expression { get; private set; }
        public UnaryExpressionKind Kind { get; private set; }

        public UnaryExpression(Expression expression, UnaryExpressionKind kind)
        {
            this.Kind = kind;
            this.AddNode(this.Expression = expression);
        }

        public override string ToString() =>
            $"{this.Kind}";
    }

    public sealed class BinaryExpression : Expression
    {
        public enum BinaryExpressionKind : int
        {
            BinaryAdd,
            BinarySub,
            BinaryMult,
            BinaryDiv,
            BinaryMod,

            BinaryRshift,
            BinaryLshift,

            BinaryLessThan,
            BinaryGreaterThan,
            BinaryLessEqThan,
            BinaryGreaterEqThan,

            BinaryLgOr,
            BinaryLgAnd,
            BinaryLgEq,
            BinaryLgNeq,

            BinaryBwOr,
            BinaryBwAnd,
            BinaryBwXor,

            BinaryAssign,
            BinaryAddAssign,
            BinarySubAssign,
            BinaryMulAssign,
            BinaryDivAssign,
            BinaryModAssign,

            BinaryBwXorAssign,
            BinaryBwOrAssign,
            BinaryBwAndAssign,
            BinaryBwNotAssign,

            BinaryLshiftAssign,
            BinaryRshiftAssign
        }

        public BinaryExpressionKind Kind { get; private set; }
        public Expression LeftExpression { get; private set; }
        public Expression RightExpression { get; private set; }

        public BinaryExpression(Expression leftExpression, Expression rightExpression, 
            BinaryExpressionKind kind)
        {
            this.Kind = kind;
            this.AddNode(this.LeftExpression = leftExpression);
            this.AddNode(this.RightExpression = rightExpression);
        }

        public override string ToString() =>
            $"{this.Kind}";
    }

    public sealed class Block : Statement
    {
        public List<Statement> Statements { get; private set; }

        public Block(List<Statement> statements) =>
            this.AddNodes((this.Statements = statements).ToArray());

        public override string ToString() =>
            "block-of-statements";
    }

    public sealed class FunctionDefinitionStatement : Statement
    {
        public Block Body { get; private set; }
        public string Name { get; private set; }
        public List<Expression> Parameters { get; private set; }

        public FunctionDefinitionStatement(string name, List<Expression> parameters, Block body)
        {
            this.Name = name;
            this.AddNodes((this.Parameters = parameters).ToArray());
            this.AddNode(this.Body = body);
        }

        public override string ToString() =>
            "func-definition-statement";
    }

    public sealed class ExpressionStatement : Statement
    {
        public Expression Expression { get; private set; }

        public ExpressionStatement(Expression expression) =>
            this.AddNode(this.Expression = expression);

        public override string ToString() =>
            "expression-statement";
    }

    public sealed class IfStatement : Statement
    {
        public sealed class ElseIfStatement : Statement
        {
            public Block Body { get; private set; }
            public Expression Condition { get; private set; }

            public ElseIfStatement(Expression condition, Block body)
            {
                this.AddNode(this.Condition = condition);
                this.AddNode(this.Body = body);
            }

            public override string ToString() =>
                "else-if-statement";
        }

        public Block Body { get; private set; }
        public Expression Condition { get; private set; }

        public Block ElseBody { get; private set; }
        public List<ElseIfStatement> ElseIfs { get; private set; }

        public IfStatement(Expression condition, Block body, 
            List<ElseIfStatement> elseIfs, Block elseBody)
        {
            this.AddNode(this.Condition = condition);
            this.AddNode(this.Body = body);

            this.AddNodes((this.ElseIfs = elseIfs).ToArray());
            this.AddNode(this.ElseBody = elseBody);
        }

        public override string ToString() =>
            this.ElseBody == null ? "if-statement" : "if-else-statement";
    }

    public sealed class SwitchStatement : Statement
    {
        public sealed class CaseStatement : Statement
        {
            public List<Statement> Body { get; private set; }
            public Expression Condition { get; private set; }

            public CaseStatement(Expression condition, List<Statement> body)
            {
                this.AddNode(this.Condition = condition);
                this.AddNodes((this.Body = body).ToArray());
            }

            public override string ToString() =>
                "case-statement";
        }

        public Expression Condition { get; private set; }
        public List<CaseStatement> Cases { get; private set; }

        public List<Statement> DefaultBody { get; private set; }

        public SwitchStatement(Expression condition, List<CaseStatement> cases,
            List<Statement> defaultBody)
        {
            this.AddNode(this.Condition = condition);
            this.AddNodes((this.Cases = cases).ToArray());
            this.AddNodes((this.DefaultBody = defaultBody).ToArray());
        }

        public override string ToString() =>
            this.DefaultBody == null ? "switch-statement" : "switch-default-statement";
    }

    public sealed class WhileLoopStatement : Statement
    {
        public Block Body { get; private set; }
        public Expression Condition { get; private set; }

        public WhileLoopStatement(Expression condition, Block body)
        {
            this.AddNode(this.Condition = condition);
            this.AddNode(this.Body = body);
        }

        public override string ToString() =>
            "while-loop-statement";
    }

    public sealed class DoLoopStatement : Statement
    {
        public Block Body { get; private set; }
        public Expression Condition { get; private set; }

        public DoLoopStatement(Expression condition, Block body)
        {
            this.AddNode(this.Body = body);
            this.AddNode(this.Condition = condition);
        }

        public override string ToString() =>
            "do-loop-statement";
    }

    public sealed class ForLoopStatement : Statement
    {
        public Block Body { get; private set; }
        public Expression Step { get; private set; }
        public Expression Condition { get; private set; }
        public Expression Initializer { get; private set; }

        public ForLoopStatement(Expression initializer, Expression condition,
            Expression step, Block body)
        {
            this.AddNode(this.Initializer = initializer);
            this.AddNode(this.Condition = condition);
            this.AddNode(this.Step = step);
            this.AddNode(this.Body = body);
        }

        public override string ToString() =>
            "for-loop-statement";
    }
}