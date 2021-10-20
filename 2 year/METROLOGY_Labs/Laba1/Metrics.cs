using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Translator.JsTranslator.Lexer;

namespace Laba1
{
    public sealed class MetricsAnalyzer
    {
        public Dictionary<string, int> Operands { get; private set; }
        public Dictionary<string, int> Operators { get; private set; }

        private List<Token> _tokens;

        public MetricsAnalyzer(List<Token> tokens)
        {
            this.Operands = new Dictionary<string, int>();
            this.Operators = new Dictionary<string, int>();
            this._tokens = tokens;
            this.Analyze();
        }

        private void Analyze()
        {
            int i = 0;
            while (i < this._tokens.Count)
            {
                Token token = this._tokens[i];
                // case of operator: +, >>, === ...
                if (this.IsOperator(token))
                    this.AddToDictionary(this.Operators,
                        new KeyValuePair<string, int>(token.Lexeme, 1));
                else if (this.IsStatement(token))
                    this.AddToDictionary(this.Operators,
                        new KeyValuePair<string, int>(token.Lexeme, 1));
                // case of operand (identifer)
                else if (this.Match(i, TokenKind.TokenIdentifier))
                    this.AddToDictionary(this.Operands,
                        new KeyValuePair<string, int>(token.Lexeme, 1));
                // case of: function identifer()
                else if (this.Match(i, TokenKind.TokenKeywordFunction))
                {
                    if (this.Match(i+1, TokenKind.TokenIdentifier))
                        if (this.Match(i+2, TokenKind.TokenOpParen))
                            i+=2; // skip name and op paren 
                }
                // case of: identifer()
                else if (this.Match(i, TokenKind.TokenIdentifier))
                {
                    if (this.Match(i+1, TokenKind.TokenOpParen))
                    {
                        this.AddToDictionary(this.Operands,
                            new KeyValuePair<string, int>($"{token.Lexeme}()", 1));
                        i++; // skip op paren
                    }
                }
                // case of: var|let|const identifer
                else if (this.IsVarDeclarator(token))
                    if (this.Match(i+1, TokenKind.TokenIdentifier))
                        i+=1; // skip var|let|const keyword
                i++;
            }
        }

        private bool Match(int i, TokenKind kind)
        {
            if (i < 0 || i >= this._tokens.Count)
                return false;
            return this._tokens[i].Kind == kind;
        }

        private bool IsVarDeclarator(Token token) =>
            token.Kind == TokenKind.TokenKeywordVar ||
            token.Kind == TokenKind.TokenKeywordLet ||
            token.Kind == TokenKind.TokenKeywordConst;

        private bool IsOperator(Token token)
        {
            // { },[ ] and ( ) are single operators, so in their case, just do nothing
            if (token.Kind == TokenKind.TokenClParen   ||
                token.Kind == TokenKind.TokenClBracket ||
                token.Kind == TokenKind.TokenClBrace)
                return false;
            if (token.Kind >= TokenKind.TokenPlus &&
                token.Kind <= TokenKind.TokenAssign)
                return true;
            if (token.Kind >= TokenKind.TokenInc &&
                token.Kind <= TokenKind.TokenRUShiftAssign)
                return true;
            if (token.Kind == TokenKind.TokenKeywordAwait      ||
                token.Kind == TokenKind.TokenKeywordNew        ||
                token.Kind == TokenKind.TokenKeywordDelete     ||
                token.Kind == TokenKind.TokenKeywordIn         ||
                token.Kind == TokenKind.TokenKeywordInstanceof ||
                token.Kind == TokenKind.TokenKeywordThrow      ||
                token.Kind == TokenKind.TokenKeywordTypeof)
                return true;
            return false;
        }

        private bool IsStatement(Token token)
        {
            if (token.Kind == TokenKind.TokenKeywordTry      ||
                token.Kind == TokenKind.TokenKeywordSwitch   ||
                token.Kind == TokenKind.TokenKeywordClass    ||
                token.Kind == TokenKind.TokenKeywordDo       ||
                token.Kind == TokenKind.TokenKeywordEnum     ||
                token.Kind == TokenKind.TokenKeywordFor      ||
                token.Kind == TokenKind.TokenKeywordIf       ||
                token.Kind == TokenKind.TokenKeywordBreak    ||
                token.Kind == TokenKind.TokenKeywordContinue ||
                token.Kind == TokenKind.TokenKeywordWhile    ||
                token.Kind == TokenKind.TokenKeywordWith     ||
                token.Kind == TokenKind.TokenKeywordYield)
                    return true;
            return false;
        }

        private void AddToDictionary(Dictionary<string, int> dictionary, KeyValuePair<string, int> pair)
        {
            if (dictionary.ContainsKey(pair.Key))
                dictionary[pair.Key] += 1;
            else
                dictionary[pair.Key] = 1;
        }
    }
}
