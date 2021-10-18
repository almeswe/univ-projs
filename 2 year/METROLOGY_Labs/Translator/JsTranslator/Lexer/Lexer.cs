using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

using Translator.JsTranslator.Exceptions;
using Translator.JsTranslator.Lexer.Input;
using Translator.JsTranslator.SpecificCharExtensions;

namespace Translator.JsTranslator.Lexer
{
    public sealed class Lexer
    {
        private static char[] _chars =
        {
            '+',
            '-',
            '*',
            '/',
            '%',
            '(',
            ')',
            '[',
            ']',
            '{',
            '}',
            '?',
            ':',
            ';',
            '.',
            '!',
            '<',
            '>',
            '|',
            '&',
            '^',
            '~',
            '=',
            '\'',
            '\"',
        };
        private static string[] _keywords =
        {
            "await",
            "break",
            "case",
            "catch",
            "class",
            "const",
            "continue",
            "debugger",
            "default",
            "delete",
            "do",
            "else",
            "enum",
            "export",
            "extends",
            "false",
            "finally",
            "for",
            "function",
            "if",
            "implements",
            "import",
            "in",
            "instanceof",
            "interface",
            "let",
            "new",
            "null",
            "package",
            "private",
            "protected",
            "public",
            "return",
            "super",
            "switch",
            "static",
            "this",
            "throw",
            "try",
            "true",
            "typeof",
            "var",
            "void",
            "while",
            "with",
            "yield",
        };
        private static string[] _keychars =
        {
            "++",
            "--",
            "<<",
            ">>",
            ">>>",

            "==",
            "!=",
            ">=",
            "<=",
            "&&",
            "||",
            "===",

            "+=",
            "-=",
            "*=",
            "/=",
            "%=",
        };

        private uint _currentLine;
        private uint _currentLineOffset;

        private uint _previousLine;
        private uint _previousLineOffset;

        private ILexerInput _input;

        public string CurrentFile => this._input.File;

        public Lexer(ILexerInput input)
        {
            this._input = input;
            this._currentLine =
                this._currentLineOffset = 1;
        }

        public List<Token> Tokenize()
        {
            var tokens = new List<Token>();

            while (!this.Eoi())
            {
                char current = this.GetCurrentChar();
                if (current.IsDigit())
                    tokens.Add(this.GetNumberToken());
                else if (current.IsDQuote())
                    tokens.Add(this.GetStringToken());
                else if (current.IsSQuote())
                    tokens.Add(this.GetCharacterToken());
                else if (current.IsForIdnt())
                    tokens.Add(this.GetIdentifierToken());
                else if (current.InSequence(_chars) >= 0)
                    tokens.Add(this.GetKeycharToken(current.InSequence(_chars)));
                this.GetNextChar();
            }
            return tokens;
        }

        private bool Eoi() =>
            this._input.Eoi();
        private bool Soi() =>
            this._input.Soi();

        private char GetNextChar()
        {
            if (this.Eoi())
                return (char)0;
            else
            {
                switch (this.GetCurrentChar())
                {
                    case '\n':
                        this._previousLine =
                            this._currentLine++;
                        break;
                    case '\r':
                        this._previousLineOffset =
                            this._currentLineOffset;
                        this._currentLineOffset = 1;
                        break;
                    // tab size is not defined, so just increment in this case
                    //case '\t'
                    default:
                        this._currentLineOffset++;
                        break;
                }
                return this._input.GetNextChar();
            }
        }
        private char GetCurrentChar() =>
            this._input.GetCurrentChar();
        private char UngetCurrentChar()
        {
            if (this.Soi())
                return (char)0;
            else
            {
                char current =
                    this._input.UngetCurrentChar();
                switch (current)
                {
                    case '\n':
                        this._currentLine =
                            this._previousLine;
                        break;
                    case '\r':
                        this._currentLineOffset =
                            this._previousLineOffset;
                        break;
                    default:
                        this._currentLineOffset--;
                        break;
                }
                return current;
            }
        }

        private Token GetNumberToken() =>
            // for possible future extensions
            this.GetDecNumberToken();
        private Token GetDecNumberToken()
        {
            char current;
            uint start = this._currentLineOffset;
            StringBuilder value =
                new StringBuilder();
            value.Append(this.GetCurrentChar());

            while ((current = this.GetNextChar()).IsForNum())
            {
                if (current.Is('.'))
                    return this.GetFloatDecNumberToken(value, start);
                value.Append(current);
            }
            this.UngetCurrentChar();
            return new Token(TokenKind.TokenNumber, value.ToString(),
                new SourceContext(this._currentLine, (uint)value.Length,
                    start, this.CurrentFile));
        }
        private Token GetFloatDecNumberToken(StringBuilder decPart, uint decStart)
        {
            char current;
            uint decPartSize =
                (uint)decPart.Length;
            decPart.Append('.');

            while ((current = this.GetNextChar()).IsDigit())
                decPart.Append(current);
            if (decPart.Length - decPartSize <= 1)
                throw new LexerException("Bad float point number format!",
                    new SourceContext(this._currentLine, (uint)decPart.Length,
                        decStart, this.CurrentFile));
            this.UngetCurrentChar();
            return new Token(TokenKind.TokenFNumber, decPart.ToString(),
                new SourceContext(this._currentLine, (uint)decPart.Length,
                    decStart, this.CurrentFile));
        }

        private Token GetIdentifierToken()
        {
            char current;
            uint start = this._currentLineOffset;
            StringBuilder value =
                new StringBuilder();
            value.Append(this.GetCurrentChar());

            while ((current = this.GetNextChar()).IsForIdntInt())
                value.Append(current);
            this.UngetCurrentChar();

            if (this.IsKeyword(value.ToString()))
                return GetKeywordToken(value, start);

            return new Token(TokenKind.TokenIdentifier, value.ToString(),
                new SourceContext(this._currentLine, (uint)value.Length,
                    start, this.CurrentFile));
        }
        private Token GetCharacterToken()
        {
            int escapeChar;
            uint start = this._currentLineOffset;
            this.GetNextChar(); // skip ' here
            bool isEscape = (escapeChar = this.IsEscapeSecuence()) >= 0;
            char current = isEscape ? (char)escapeChar : this.GetCurrentChar();
            if (!this.GetNextChar().Is('\''))
                throw new LexerException("Expected single quote at the end of literal.",
                    new SourceContext(this._currentLine, (uint)(isEscape ? 4 : 3), start, this.CurrentFile));

            return new Token(TokenKind.TokenChar, current.ToString(),
                new SourceContext(this._currentLine, (uint)(isEscape ? 4 : 3), start, this.CurrentFile));
        }
        private Token GetStringToken()
        {
            bool isEscape;
            int escapeChar;
            uint start = this._currentLineOffset;
            uint additionalSize = 2; // 2 double quotes
            StringBuilder str = new StringBuilder();

            while (!this.Eoi() && this.GetNextChar().IsForStr())
            {
                isEscape = (escapeChar = this.IsEscapeSecuence()) >= 0;
                str.Append(isEscape ? (char)escapeChar : this.GetCurrentChar());
                if (isEscape)
                    additionalSize++;
            }
            if (!this.GetCurrentChar().Is('\"'))
                throw new LexerException("Expected double quote at the end of string.",
                    new SourceContext(this._currentLine, (uint)str.Length + additionalSize, start, this.CurrentFile));

            return new Token(TokenKind.TokenString, str.ToString(),
                new SourceContext(this._currentLine, (uint)str.Length + additionalSize, start, this.CurrentFile));
        }

        private Token GetKeycharToken(int charInSequence)
        {
            int type = -1;
            int index = -1;
            bool found = true;
            uint start = this._currentLineOffset;
            StringBuilder str = new StringBuilder();
            str.Append(_chars[charInSequence]);

            while (found && !this.Eoi())
            {
                found = false;
                str.Append(this.GetNextChar());
                if (found = this.IsKeychar(str.ToString()))
                    index = Array.FindIndex(_keychars, i => i == str.ToString());
                type = index >= 0 ? index : type;
            }
            this.UngetCurrentChar();
            str.Remove(str.Length - 1, 1);

            TokenKind kind = (TokenKind)(str.Length == 1 ?
                charInSequence : type + (int)TokenKind.TokenInc);
            return new Token(kind, str.ToString(),
                new SourceContext(this._currentLine, (uint)str.Length, start, this.CurrentFile));
        }
        private Token GetKeywordToken(StringBuilder keyword, uint start)
        {
            int index = 0;
            for (index = 0; index < _keywords.Length; index++)
                if (_keywords[index] == keyword.ToString())
                    break;
            TokenKind kind = (TokenKind)((int)TokenKind.TokenKeywordAwait + index);
            return new Token(kind, keyword.ToString(),
                new SourceContext(this._currentLine, (uint)keyword.Length,
                    start, this.CurrentFile));
        }

        private int IsEscapeSecuence()
        {
            if (this.GetCurrentChar().Is('\\'))
            {
                switch (this.GetNextChar())
                {
                    case 'a':
                        return '\a';
                    case 'b':
                        return '\b';
                    case 'f':
                        return '\f';
                    case 'n':
                        return '\n';
                    case 'r':
                        return '\r';
                    case 't':
                        return '\t';
                    case 'v':
                        return '\v';
                    case '0':
                        return '\0';
                    case '\\':
                        return '\\';
                    case '\'':
                        return '\'';
                    case '\"':
                        return '\"';
                    default:
                        this.UngetCurrentChar();
                        break;
                }
            }
            return -1;
        }

        private bool IsKeychar(string keychar) =>
            _keychars.FirstOrDefault(k => k == keychar) != null;
        private bool IsKeyword(string identifier) =>
            _keywords.FirstOrDefault(k => k == identifier) != null;
    }
}