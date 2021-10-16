using System;
using Translator.JsTranslator.Exceptions;

namespace Translator.JsTranslator.Lexer.Input
{
    public sealed class FileInput : ILexerInput
    {
        private StringInput _input;

        public bool IsFile => true;
        public string File { get; private set; }

        public FileInput(string file)
        {
            if (!System.IO.File.Exists(this.File = file))
                throw new LexerException($"Specified file \'{file}\' does not exists!");
            this._input = new StringInput(System.IO.File.ReadAllText(file));
        }

        public bool Soi() =>
            this._input.Soi();

        public bool Eoi() =>
            this._input.Eoi();

        public char GetCurrentChar() =>
            this._input.GetCurrentChar();

        public char GetNextChar() =>
            this._input.GetNextChar();

        public char PeekChar() =>
            this._input.PeekChar();

        public char UngetCurrentChar() =>
            this._input.UngetCurrentChar();
    }
}