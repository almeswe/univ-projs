using System;
using System.IO;

namespace Translator.JsTranslator.Lexer.Input
{
    public class LexerException : Exception
    {
        public LexerException(string message) : base(message) { }
    }

    public sealed class FileInput : ILexerInput
    {
        public StringInput _input;

        public FileInput(string file)
        {
            if (!File.Exists(file))
                throw new LexerException($"Specified file \'{file}\' does not exists!");
            this._input = new StringInput(File.ReadAllText(file));
        }

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
