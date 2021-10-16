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
            "+", //ARITH
            "-",
            "*",
            "/",
            "%",
            "(",
            ")",
            "[",
            "]",
            "{",
            "}",
            "?",
            ":",
            ";",
            ".",
            "!",
            "<",
            ">",
            "|",
            "&",
            "^",
            "~",
            "=",
            "\'",
            "\"",

            "++",
            "--",
            "<<",
            ">>",
            ">>>",

            "==", //LOGICAL
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
                else if (current.IsForIdnt())
                    tokens.Add(this.GetIdentifierToken());
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

        private bool IsKeyword(string identifier) =>
            _keywords.FirstOrDefault(k => k == identifier) != null;
    }
}