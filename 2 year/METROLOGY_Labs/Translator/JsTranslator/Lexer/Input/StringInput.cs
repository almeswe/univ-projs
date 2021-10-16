namespace Translator.JsTranslator.Lexer.Input
{
    public sealed class StringInput : ILexerInput
    {
        private int _currentIndex = 0;

        public string Input { get; private set; }
        public bool IsFile => false;
        public string File => "undefined";

        public StringInput(string input) : base() => 
            this.Input = input;

        public bool Soi() =>
            this._currentIndex <= 0;

        public bool Eoi() =>
            this._currentIndex >= this.Input.Length-1;

        public char GetCurrentChar() =>
            this.Input[this._currentIndex];

        public char GetNextChar() =>
            !this.Eoi() ? this.Input[++this._currentIndex] 
                : this.GetCurrentChar();

        public char PeekChar() => 
            (this._currentIndex + 1 < this.Input.Length) ?
                this.Input[this._currentIndex + 1] : this.GetCurrentChar();

        public char UngetCurrentChar() =>
            this._currentIndex > 0 ? this.Input[--this._currentIndex] 
                : this.GetCurrentChar();
    }
}
