using System.Collections.Generic;

using GeometryScript.FrontEnd.Tokenizer;

namespace GeometryScript.FrontEnd.Script.Parser
{
    public sealed class ScriptParser
    {
        private Token _currentToken;
        private IEnumerable<Token> _tokens;

        public Script Script => this.Parse();

        public ScriptParser(IEnumerable<Token> tokens) =>
            this._tokens = tokens;

        private Script Parse() =>
            new Script(this.ParseScriptLines());

        private IEnumerable<ScriptLine> ParseScriptLines()
        {
            this.GetNextToken();
            while (!this.Match(TokenKind.TokenEOF))
            {
                this.Expect(TokenKind.TokenShape);
                yield return new ScriptLine(this._currentToken.Lexeme,
                    this.ParseScriptLineOptions());
            }
        }

        private IEnumerable<ScriptLineOption> ParseScriptLineOptions()
        {
            this.GetNextToken();
            while (!this.Match(TokenKind.TokenEOF) &&
                !this.Match(TokenKind.TokenSemicolon))
            {
                this.Expect(TokenKind.TokenArgument);
                yield return new ScriptLineOption(new ScriptLineArgument(
                    this._currentToken.Lexeme), this.ParseScriptLineValues());
            }
            if (this.Match(TokenKind.TokenSemicolon))
                this.GetNextToken();
        }

        private IEnumerable<ScriptLineValue> ParseScriptLineValues()
        {
            this.GetNextToken();
            this.Expect(TokenKind.TokenColon);
            this.GetNextToken();
            while (!this.Match(TokenKind.TokenEOF) && 
                !this.Match(TokenKind.TokenDot) &&
                this.Match(TokenKind.TokenValue))
            {
                int value;
                this.Expect(TokenKind.TokenValue);
                if (!int.TryParse(this._currentToken.Lexeme, out value))
                    throw new System.ArgumentException($"Cannot evaluate option's value, " +
                        $"{this._currentToken.Context}");
                yield return new ScriptLineValue(value);
                this.GetNextToken();
                if (this.Match(TokenKind.TokenComma))
                    this.GetNextToken();
            }
            if (this.Match(TokenKind.TokenDot))
                this.GetNextToken();
        }

        private bool Match(TokenKind kind) =>
            kind == this._currentToken.Kind;

        private void Expect(TokenKind kind)
        {
            if (!this.Match(kind))
                throw new System.ArgumentException($"{kind} expected here, but met " +
                    $"{this._currentToken.Kind}, {this._currentToken.Context}");
        }

        private Token GetNextToken()
        {
            if (this._currentToken != null)
                if (this._currentToken.Kind ==
                    TokenKind.TokenEOF)
                        return this._currentToken;
            var enumerator = this._tokens.GetEnumerator();
            enumerator.MoveNext();
            return this._currentToken =
                enumerator.Current;
        }
    }
}