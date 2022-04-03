using System.Collections.Generic;

namespace GeometryScript.FrontEnd.Input
{
    public sealed class StringInput : ITokenizerInput
    {
        private int _streamSize;

        private int _currentChar = 0;
        private int _currentLine = 1;
        private int _currentCharInLine = 1;

        private Stack<int> _cachedOffsets = new Stack<int>();

        public string Stream { get; private set; }

        public StringInput(string stream) =>
            this._streamSize = (this.Stream = stream).Length;

        public char ReadChar()
        {
            if (this._currentChar >= this._streamSize)
                return '\0';
            switch (this.Stream[this._currentChar])
            {
                case '\n':
                    this._currentLine++;
                    break;
                case '\r':
                    this._cachedOffsets.Push(
                        this._currentCharInLine);
                    this._currentCharInLine = 1;
                    break;
            }
            return this.Stream[this._currentChar++];
        }

        public bool UnreadChar()
        {
            if (this._currentChar <= 0)
                return false;
            switch (this.Stream[--this._currentChar])
            {
                case '\n':
                    this._currentLine--;
                    break;
                case '\r':
                    this._currentCharInLine = 
                        this._cachedOffsets.Pop();
                    break;
            }
            return true;
        }

        public string ReadWhole() =>
            this.Stream;

        public Context GetCurrentContext() => new Context()
        {
            File = null,
            IsInFile = false,
            Line = this._currentLine,
            LineOffset = this._currentCharInLine
        };
    }
}