using System;
using System.Collections.Generic;

using Translator.JsTranslator.Lexer;

namespace Translator.JsTranslator.Syntax
{
    public sealed class Parser
    {
        public List<Token> Tokens { get; private set; }
        public int CurrentTokenIndex { get; private set; }

        public Parser(List<Token> tokens)
        {
            this.Tokens = tokens;
            this.CurrentTokenIndex = 0;
        }

        private bool Match(TokenKind kind, int offset = 0)
        {
            if (this.CurrentTokenIndex >= 0 &&
                this.CurrentTokenIndex < this.Tokens.Count)
                return this.Tokens[this.CurrentTokenIndex + offset].Kind == kind;
            return false;
        }

        private Token GetNextToken()
        {
            if (this.CurrentTokenIndex + 1 >= 0 &&
                this.CurrentTokenIndex + 1 < this.Tokens.Count)
                return this.Tokens[++this.CurrentTokenIndex];
            return this.GetCurrentToken();
        }

        private Token GetCurrentToken()
        {
            if (this.CurrentTokenIndex >= 0 &&
                this.CurrentTokenIndex < this.Tokens.Count)
                return this.Tokens[this.CurrentTokenIndex];
            throw new ArgumentNullException();
        }

        private Token UngetCurrentToken()
        {
            if (this.CurrentTokenIndex - 1 >= 0 &&
                this.CurrentTokenIndex - 1 < this.Tokens.Count)
                return this.Tokens[--this.CurrentTokenIndex];
            return this.GetCurrentToken();
        }

        private void ExpectWithSkip(TokenKind kind)
        {
            if (!Match(kind))
                throw new Exception($"Expected {kind} token, met: " +
                    $"{this.GetCurrentToken().Kind}");
            this.GetNextToken();
        }

        public AstRoot Parse()
        {
            var statements = new List<Statement>();
            while (!this.Match(TokenKind.TokenEOF))
                statements.Add(this.ParseStatement());
            return new AstRoot(statements);
        }

        private Block ParseBlock()
        {
            var statements = new List<Statement>();
            this.ExpectWithSkip(TokenKind.TokenOpBrace);
            while (!this.Match(TokenKind.TokenClBrace))
                statements.Add(ParseStatement());
            this.ExpectWithSkip(TokenKind.TokenClBrace);
            return new Block(statements);
        }

        private Statement ParseStatement()
        {
            switch (this.GetCurrentToken().Kind)
            {
                case TokenKind.TokenKeywordIf:
                    return this.ParseIfStatement();
                case TokenKind.TokenKeywordDo:
                    return this.ParseDoLoopStatement();
                case TokenKind.TokenKeywordFor:
                    return this.ParseForLoopStatement();
                case TokenKind.TokenKeywordWhile:
                    return this.ParseWhileLoopStatement();
                case TokenKind.TokenKeywordSwitch:
                    return this.ParseSwitchStatement();
                case TokenKind.TokenKeywordFunction:
                    return this.ParseFunctionDefinitionStatement();
                default:
                    return this.ParseExpressionStatement();
            }
        }

        private Statement ParseExpressionStatement()
        {
            Expression expression = ParseExpression();
            ExpectWithSkip(TokenKind.TokenSemicolon);
            return new ExpressionStatement(expression);
        }

        private Statement ParseElseIfStatement()
        {
            Block body;
            Expression condition;

            this.ExpectWithSkip(TokenKind.TokenKeywordElse);
            this.ExpectWithSkip(TokenKind.TokenKeywordIf);
            this.ExpectWithSkip(TokenKind.TokenOpParen);
            condition = ParseExpression();
            this.ExpectWithSkip(TokenKind.TokenClParen);
            body = ParseBlock();

            return new IfStatement.ElseIfStatement(condition, body);
        }

        private Statement ParseIfStatement()
        {
            Block body;
            Block elseBody = null;
            Expression condition;
            var branches = new List<IfStatement.ElseIfStatement>();

            this.ExpectWithSkip(TokenKind.TokenKeywordIf);
            this.ExpectWithSkip(TokenKind.TokenOpParen);
            condition = ParseExpression();
            this.ExpectWithSkip(TokenKind.TokenClParen);
            body = ParseBlock();

            while (this.Match(TokenKind.TokenKeywordElse))
            {
                if (this.Match(TokenKind.TokenKeywordIf, 1))
                    branches.Add((IfStatement.ElseIfStatement)
                        ParseElseIfStatement());
                else
                {
                    this.GetNextToken();
                    elseBody = ParseBlock();
                    break;
                }
            }

            return new IfStatement(condition, body, 
                branches, elseBody);
        }

        private Statement ParseDoLoopStatement()
        {
            Block body;
            Expression condition;

            this.ExpectWithSkip(TokenKind.TokenKeywordDo);
            body = this.ParseBlock();
            this.ExpectWithSkip(TokenKind.TokenKeywordWhile);
            condition = this.ParseParenExpression();
            this.ExpectWithSkip(TokenKind.TokenSemicolon);

            return new DoLoopStatement(condition, body);
        }

        private Statement ParseWhileLoopStatement()
        {
            Block body;
            Expression condition;

            this.ExpectWithSkip(TokenKind.TokenKeywordWhile);
            condition = this.ParseParenExpression();
            body = this.ParseBlock();

            return new WhileLoopStatement(condition, body);
        }

        private Statement ParseForLoopStatement()
        {
            Block body;
            Expression step;
            Expression condition;
            Expression initializer;

            this.ExpectWithSkip(TokenKind.TokenKeywordFor);
            this.ExpectWithSkip(TokenKind.TokenOpParen);
            initializer = this.ParseExpression();
            this.ExpectWithSkip(TokenKind.TokenSemicolon);
            condition = this.ParseExpression();
            this.ExpectWithSkip(TokenKind.TokenSemicolon);
            step = this.ParseExpression();
            this.ExpectWithSkip(TokenKind.TokenClParen);
            body = ParseBlock();

            return new ForLoopStatement(initializer, condition, step, body);
        }

        private List<Statement> ParseCaseStatementBody()
        {
            var statememts = new List<Statement>();

            while (!this.Match(TokenKind.TokenEOF) && !this.Match(TokenKind.TokenKeywordBreak))
                statememts.Add(this.ParseStatement());

            if (!this.Match(TokenKind.TokenEOF))
            {
                this.ExpectWithSkip(TokenKind.TokenKeywordBreak);
                this.ExpectWithSkip(TokenKind.TokenSemicolon);
            }
            return statememts;
        }

        private SwitchStatement.CaseStatement ParseCaseStatement()
        {
            List<Statement> body;
            Expression condition;
            this.ExpectWithSkip(TokenKind.TokenKeywordCase);
            condition = this.ParseExpression();
            this.ExpectWithSkip(TokenKind.TokenColon);
            body = this.ParseCaseStatementBody();

            return new SwitchStatement.CaseStatement(condition, body);
        }

        private Statement ParseSwitchStatement()
        {
            Expression condition;
            var defaultBody = new List<Statement>();
            var cases = new List<SwitchStatement.CaseStatement>();

            this.ExpectWithSkip(TokenKind.TokenKeywordSwitch);
            condition = this.ParseParenExpression();
            this.ExpectWithSkip(TokenKind.TokenOpBrace);

            while (this.Match(TokenKind.TokenKeywordCase) ||
                   this.Match(TokenKind.TokenKeywordDefault))
            {
                if (this.Match(TokenKind.TokenKeywordCase))
                    cases.Add(this.ParseCaseStatement());
                else
                {
                    this.ExpectWithSkip(TokenKind.TokenKeywordDefault);
                    this.ExpectWithSkip(TokenKind.TokenColon);
                    defaultBody = this.ParseCaseStatementBody();
                    break;
                }
            }

            this.ExpectWithSkip(TokenKind.TokenClBrace);

            return new SwitchStatement(condition, cases, defaultBody);
        }

        private Statement ParseFunctionDefinitionStatement()
        {
            Block body;
            string name;
            var parameters = new List<Expression>();
            
            this.ExpectWithSkip(TokenKind.TokenKeywordFunction);
            name = this.GetCurrentToken().Lexeme;
            this.ExpectWithSkip(TokenKind.TokenIdentifier);
            this.ExpectWithSkip(TokenKind.TokenOpParen);
            do
            {
                if (this.Match(TokenKind.TokenComma))
                    this.ExpectWithSkip(TokenKind.TokenComma);
                if (!this.Match(TokenKind.TokenClParen))
                    parameters.Add(this.ParseExpression());
            } while (this.Match(TokenKind.TokenComma));
            this.ExpectWithSkip(TokenKind.TokenClParen);
            body = this.ParseBlock();

            return new FunctionDefinitionStatement(name, parameters, body);
        }

        private Expression ParseExpression() =>
            this.ParseAssignmentExpression();

        private Expression ParseParenExpression()
        {
            this.ExpectWithSkip(TokenKind.TokenOpParen);
            var expression = this.ParseExpression();
            this.ExpectWithSkip(TokenKind.TokenClParen);
            return expression;
        }

        private Expression ParseFunctionCallExpression()
        {
            string name = this.GetCurrentToken().Lexeme;
            var args = new List<Expression>();

            this.ExpectWithSkip(TokenKind.TokenIdentifier);
            this.ExpectWithSkip(TokenKind.TokenOpParen);
            do
            {
                if (this.Match(TokenKind.TokenComma))
                    this.ExpectWithSkip(TokenKind.TokenComma);
                if (!this.Match(TokenKind.TokenClParen))
                    args.Add(this.ParseExpression());
            } while (this.Match(TokenKind.TokenComma));
            this.ExpectWithSkip(TokenKind.TokenClParen);

            return new FunctionCall(name, args);
        }

        private Expression ParsePrimaryExpression()
        {
            var token = this.GetCurrentToken();

            switch(token.Kind)
            {
                case TokenKind.TokenIdentifier:
                    if (this.Match(TokenKind.TokenOpParen, 1))
                        return this.ParseFunctionCallExpression();
                    this.GetNextToken();
                    return new Identifier(token.Lexeme);

                case TokenKind.TokenNumber:
                    this.GetNextToken();
                    return new Constant(token.Lexeme, 
                        Constant.ConstantKind.IntegerConstant);

                case TokenKind.TokenFNumber:
                    this.GetNextToken();
                    return new Constant(token.Lexeme,
                        Constant.ConstantKind.RealConstant);

                case TokenKind.TokenString:
                    this.GetNextToken();
                    return new Constant(token.Lexeme,
                        Constant.ConstantKind.StringConstant);

                case TokenKind.TokenChar:
                    this.GetNextToken();
                    return new Constant(token.Lexeme,
                        Constant.ConstantKind.LiteralConstant);

                case TokenKind.TokenOpParen:
                    return this.ParseParenExpression();

                default:
                    throw new Exception($"Expected token that specifies expression, " +
                        $"but met: {token.Kind}");
            }
        }

        private Expression ParseUnaryExpression()
        {
            switch (this.GetCurrentToken().Kind)
            {
                case TokenKind.TokenPlus:
                    this.GetNextToken();
                    return new UnaryExpression(this.ParseUnaryExpression(),
                        UnaryExpression.UnaryExpressionKind.UnaryPlus);

                case TokenKind.TokenDash:
                    this.GetNextToken();
                    return new UnaryExpression(this.ParseUnaryExpression(),
                        UnaryExpression.UnaryExpressionKind.UnaryMinus);

                case TokenKind.TokenTilde:
                    this.GetNextToken();
                    return new UnaryExpression(this.ParseUnaryExpression(),
                        UnaryExpression.UnaryExpressionKind.UnaryBwNot);

                case TokenKind.TokenExlMark:
                    this.GetNextToken();
                    return new UnaryExpression(this.ParseUnaryExpression(),
                        UnaryExpression.UnaryExpressionKind.UnaryLgNot);

                default:
                    return this.ParsePrimaryExpression();
            }
        }

        private Expression ParseMultiplicationExpression()
        {
            Expression mulExpr = null;
            Expression unaryExpr = ParseUnaryExpression();

            while (this.Match(TokenKind.TokenAsterisk) ||
                   this.Match(TokenKind.TokenSlash)    ||
                   this.Match(TokenKind.TokenModulus))
            {
                TokenKind kind = this.GetCurrentToken().Kind;
                this.GetNextToken();
                mulExpr = new BinaryExpression(mulExpr != null ?
                    mulExpr : unaryExpr, ParseMultiplicationExpression(),
                        (BinaryExpression.BinaryExpressionKind)kind);
            }
            return mulExpr != null ? mulExpr : unaryExpr;
        }

        private Expression ParseAdditionExpression()
        {
            Expression addExpr = null;
            Expression mulExpr = ParseMultiplicationExpression();

            while (this.Match(TokenKind.TokenPlus) ||
                   this.Match(TokenKind.TokenDash))
            {
                TokenKind kind = this.GetCurrentToken().Kind;
                this.GetNextToken();
                addExpr = new BinaryExpression(addExpr != null ?
                    addExpr : mulExpr, ParseAdditionExpression(),
                        (BinaryExpression.BinaryExpressionKind)kind);
            }
            return addExpr != null ? addExpr : mulExpr;
        }

        private Expression ParseShiftExpression()
        {
            Expression sftExpr = null;
            Expression addExpr = ParseAdditionExpression();

            while (this.Match(TokenKind.TokenRShift) ||
                   this.Match(TokenKind.TokenLShift))
            {
                TokenKind kind = this.GetCurrentToken().Kind;
                this.GetNextToken();
                sftExpr = new BinaryExpression(sftExpr != null ?
                    sftExpr : addExpr, ParseShiftExpression(),
                        (BinaryExpression.BinaryExpressionKind)(kind - 29));
            }
            return sftExpr != null ? sftExpr : addExpr;
        }

        private Expression ParseRelationalExpression()
        {
            Expression relExpr = null;
            Expression sftExpr = ParseShiftExpression();

            while (this.Match(TokenKind.TokenLAngle) ||
                   this.Match(TokenKind.TokenRAngle) ||
                   this.Match(TokenKind.TokenLgLEqT) ||
                   this.Match(TokenKind.TokenLgGEqT))
            {
                TokenKind tkind = this.GetCurrentToken().Kind;
                this.GetNextToken();
                BinaryExpression.BinaryExpressionKind kind = 
                    BinaryExpression.BinaryExpressionKind.BinaryLessThan;
                switch (tkind)
                {
                    case TokenKind.TokenLAngle:
                        kind = BinaryExpression.BinaryExpressionKind.
                            BinaryLessThan;
                        break;
                    case TokenKind.TokenRAngle:
                        kind = BinaryExpression.BinaryExpressionKind.
                            BinaryGreaterThan;
                        break;
                    case TokenKind.TokenLgLEqT:
                        kind = BinaryExpression.BinaryExpressionKind.
                            BinaryLessEqThan;
                        break;
                    case TokenKind.TokenLgGEqT:
                        kind = BinaryExpression.BinaryExpressionKind.
                            BinaryGreaterEqThan;
                        break;
                }

                relExpr = new BinaryExpression(relExpr != null ?
                    relExpr : sftExpr, ParseRelationalExpression(),
                        kind);
            }
            return relExpr != null ? relExpr : sftExpr;
        }

        private Expression ParseEqualityExpression()
        {
            Expression equExpr = null;
            Expression relExpr = ParseRelationalExpression();

            while (this.Match(TokenKind.TokenLgEq) ||
                   this.Match(TokenKind.TokenLgNeq))
            {
                TokenKind kind = this.GetCurrentToken().Kind;
                this.GetNextToken();
                equExpr = new BinaryExpression(equExpr != null ?
                    equExpr : relExpr, ParseEqualityExpression(),
                        (BinaryExpression.BinaryExpressionKind)(kind - 24));
            }
            return equExpr != null ? equExpr : relExpr;
        }

        private Expression ParseBitwiseAndExpression()
        {
            Expression bwAndExpr = null;
            Expression equExpr = ParseEqualityExpression();

            while (this.Match(TokenKind.TokenAmpersand))
            {
                this.GetNextToken();
                bwAndExpr = new BinaryExpression(bwAndExpr != null ?
                    bwAndExpr : equExpr, ParseBitwiseAndExpression(),
                        BinaryExpression.BinaryExpressionKind.BinaryBwAnd);
            }
            return bwAndExpr != null ? bwAndExpr : equExpr;
        }

        private Expression ParseBitwiseXorExpression()
        {
            Expression bwXorExpr = null;
            Expression bwAndExpr = ParseBitwiseAndExpression();

            while (this.Match(TokenKind.TokenCaret))
            {
                this.GetNextToken();
                bwXorExpr = new BinaryExpression(bwXorExpr != null ?
                    bwXorExpr : bwAndExpr, ParseBitwiseXorExpression(),
                        BinaryExpression.BinaryExpressionKind.BinaryBwXor);
            }
            return bwXorExpr != null ? bwXorExpr : bwAndExpr;
        }

        private Expression ParseBitwiseOrExpression()
        {
            Expression bwOrExpr = null;
            Expression bwXorExpr = ParseBitwiseXorExpression();

            while (this.Match(TokenKind.TokenBar))
            {
                this.GetNextToken();
                bwOrExpr = new BinaryExpression(bwOrExpr != null ?
                    bwOrExpr : bwXorExpr, ParseBitwiseOrExpression(),
                        BinaryExpression.BinaryExpressionKind.BinaryBwOr);
            }
            return bwOrExpr != null ? bwOrExpr : bwXorExpr;
        }

        private Expression ParseLogicalAndExpression()
        {
            Expression andExpr = null;
            Expression bwOrExpr = ParseBitwiseOrExpression();

            while (this.Match(TokenKind.TokenLgAnd))
            {
                this.GetNextToken();
                andExpr = new BinaryExpression(andExpr != null ?
                    andExpr : bwOrExpr, ParseLogicalAndExpression(),
                        BinaryExpression.BinaryExpressionKind.BinaryLgAnd);
            }
            return andExpr != null ? andExpr : bwOrExpr;
        }

        private Expression ParseLogicalOrExpression()
        {
            Expression orExpr = null;
            Expression andExpr = ParseLogicalAndExpression();

            while (this.Match(TokenKind.TokenLgOr))
            {
                this.GetNextToken();
                orExpr = new BinaryExpression(orExpr != null ?
                    orExpr : andExpr, ParseLogicalOrExpression(),
                        BinaryExpression.BinaryExpressionKind.BinaryLgOr);
            }
            return orExpr != null ? orExpr : andExpr;
        }

        private Expression ParseAssignmentExpression()
        {
            Expression assignExpr = null;
            Expression orExpr = ParseLogicalOrExpression();

            while (this.Match(TokenKind.TokenAssign)
                || this.Match(TokenKind.TokenAddAssign)
                || this.Match(TokenKind.TokenSubAssign)
                || this.Match(TokenKind.TokenMulAssign)
                || this.Match(TokenKind.TokenDivAssign)
                || this.Match(TokenKind.TokenModAssign)
                || this.Match(TokenKind.TokenLShiftAssign)
                || this.Match(TokenKind.TokenRShiftAssign)
                || this.Match(TokenKind.TokenBwNotAssign)
                || this.Match(TokenKind.TokenBwOrAssign)
                || this.Match(TokenKind.TokenBwAndAssign)
                || this.Match(TokenKind.TokenBwXorAssign))
            {
                TokenKind kind = this.GetCurrentToken().Kind;
                this.GetNextToken();
                assignExpr = new BinaryExpression(assignExpr != null ?
                    assignExpr : orExpr, ParseAssignmentExpression(),
                        (BinaryExpression.BinaryExpressionKind)(kind == TokenKind.TokenAssign ? (kind - 5) : (kind - 25)));
            }

            return assignExpr != null ? assignExpr : orExpr;
        }
    }
}
