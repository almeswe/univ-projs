using System.Text;
using System.Collections.Generic;

using GeometryScript.FrontEnd.Input;

namespace GeometryScript.FrontEnd.Tokenizer
{
    public sealed class Tokenizer
    {
        private char _currentChar;
        private ITokenizerInput _input;

        private string[] _shapes =
        {
            "Ellipse",
            "Circle",
            "Dot",
            "Polygon",
            "Line",
            "Triangle",
            "Rect",
            "Square"
        };

        public IEnumerable<Token> Tokens => this.GetTokens();

        public Tokenizer(ITokenizerInput input) =>
            this._input = input;

        private IEnumerable<Token> GetTokens()
        {
            while ((this._currentChar = this._input
                .ReadChar()) != '\0')
            {
                if (this._currentChar == '%')
                    yield return this.GetShapeToken();
                else if (char.IsDigit(this._currentChar))
                    yield return this.GetValueToken();
                else if (char.IsLetter(this._currentChar))
                    yield return this.GetArgumentToken();
                else if (char.IsWhiteSpace(this._currentChar) ||
                    char.IsControl(this._currentChar))
                        continue;
                else
                    yield return this.GetCharToken();
            }
            yield return this.GetEOFToken();
        }

        private Token GetShapeToken()
        {
            var lexeme = new StringBuilder(string.Empty);
            var context = this._input.GetCurrentContext();
            while (this.GetNextChar() != '\0' && 
                   char.IsLetter(this._currentChar))
                lexeme.Append(this._currentChar);
            if (lexeme.Length == 0)
                throw new System.ArgumentException(
                    $"Shape cannot be empty, {context}.");
            this.UngetChar();
            return new Token(lexeme.ToString(), 
                TokenKind.TokenShape, context);
        }

        private Token GetArgumentToken()
        {
            var lexeme = new StringBuilder(
                $"{this._currentChar}");
            var context = this._input.GetCurrentContext();

            while (this.GetNextChar() != '\0' &&
                   char.IsLetter(this._currentChar))
                lexeme.Append(this._currentChar);
            if (lexeme.Length == 0)
                throw new System.ArgumentException(
                    $"Argument cannot be empty, {context}.");
            this.UngetChar();
            return new Token(lexeme.ToString(),
                TokenKind.TokenArgument, context);
        }

        private Token GetValueToken()
        {
            var lexeme = new StringBuilder(
                $"{this._currentChar}");
            var context = this._input.GetCurrentContext();

            while (this.GetNextChar() != '\0' &&
                   char.IsDigit(this._currentChar))
                lexeme.Append(this._currentChar);
            if (lexeme.Length == 0)
                throw new System.ArgumentException(
                    $"Value cannot be empty, {context}.");
            this.UngetChar();
            return new Token(lexeme.ToString(),
                TokenKind.TokenValue, context);
        }

        private Token GetCharToken()
        {
            var context = this._input.GetCurrentContext();
            switch (this._currentChar)
            {
                case '.':
                    return new Token($"{this._currentChar}",
                        TokenKind.TokenDot, context);
                case ':':
                    return new Token($"{this._currentChar}", 
                        TokenKind.TokenColon, context);
                case ';':
                    return new Token($"{this._currentChar}",
                        TokenKind.TokenSemicolon, context);
                case ',':
                    return new Token($"{this._currentChar}",
                        TokenKind.TokenComma, context);
                default:
                    throw new System.ArgumentException(
                        $"Unknown character met [{this._currentChar}, " +
                            $"{(int)this._currentChar}], {context}");
            }
        }

        private Token GetEOFToken() =>
            new Token("", TokenKind.TokenEOF,
                this._input.GetCurrentContext());

        private void UngetChar()
        {
            if (this._currentChar != '\0')
                this._input.UnreadChar();
        }

        private char GetNextChar() =>
            this._currentChar = this._input.ReadChar();
    }
}