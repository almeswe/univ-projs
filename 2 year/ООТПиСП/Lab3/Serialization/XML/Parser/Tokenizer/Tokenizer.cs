using System.Text;
using System.Collections.Generic;
using Serialization.XML.Parser.Exceptions;

namespace Serialization.XML.Parser
{
    public sealed class Tokenizer
    {
        private char _currentChar;
        private ITokenizerInput _input;

        public IEnumerable<Token> Tokens => this.GetTokens();

        public Tokenizer(ITokenizerInput input) =>
            this._input = input;

        private IEnumerable<Token> GetTokens()
        {
            while ((this._currentChar = this._input
                .ReadChar()) != '\0')
            {
                if (this._currentChar == '\"')
                    yield return this.GetStringToken();
                else if (char.IsLetter(this._currentChar))
                    yield return this.GetIdentifierToken();
                else if (char.IsWhiteSpace(this._currentChar) ||
                    char.IsControl(this._currentChar))
                    continue;
                else
                    yield return this.GetCharToken();
            }
            yield return this.GetEOFToken();
        }

        private Token GetEOFToken() =>
            new Token("", TokenKind.TokenEOF,
                this._input.GetCurrentContext());

        private Token GetCharToken()
        {
            var context = this._input.GetCurrentContext();
            switch (this._currentChar)
            {
                case '<':
                    return new Token($"{this._currentChar}",
                        TokenKind.TokenLeftAngle, context);
                case '>':
                    return new Token($"{this._currentChar}",
                        TokenKind.TokenRightAngle, context);
                case '=':
                    return new Token($"{this._currentChar}",
                        TokenKind.TokenAssign, context);
                case '?':
                    return new Token($"{this._currentChar}",
                        TokenKind.TokenQuestion, context);
                case '/':
                    return new Token($"{this._currentChar}",
                        TokenKind.TokenSlash, context);
                default:
                    throw new XMLParserException(
                        $"Unknown character met [{this._currentChar}, " +
                            $"{(int)this._currentChar}], {context}");
            }
        }

        private Token GetStringToken()
        {
            var lexeme = new StringBuilder(string.Empty);
            var context = this._input.GetCurrentContext();
            while (this.GetNextChar() != '\0' && this._currentChar != '\"')
                lexeme.Append(this._currentChar);
            if (this._currentChar != '\"')
                this.UngetChar();
            return new Token(lexeme.ToString(),
                TokenKind.TokenString, context);
        }

        private Token GetIdentifierToken()
        {
            var lexeme = new StringBuilder($"{this._currentChar}");
            var context = this._input.GetCurrentContext();
            while (this.GetNextChar() != '\0' && 
                    char.IsLetterOrDigit(this._currentChar))
                lexeme.Append(this._currentChar);
            this.UngetChar();
            return new Token(lexeme.ToString(),
                TokenKind.TokenIdentifier, context);
        }

        private void UngetChar()
        {
            if (this._currentChar != '\0')
                this._input.UnreadChar();
        }

        private char GetNextChar() =>
            this._currentChar = this._input.ReadChar();
    }
}