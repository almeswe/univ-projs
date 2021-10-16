using System;

namespace Translator.JsTranslator.Exceptions
{
    public enum ErrorSourceKind
    {
        Lexing
        //Syntax,
        //Semantic
    }

    public abstract class TranslatorException : Exception
    {
        public SourceContext Context { get; private set; }
        public TranslatorException(string message, SourceContext context) : base(message) =>
            this.Context = context;

        public override string ToString() =>
            $"{this.Message} " + (this.Context?.ToString());
    }

    public sealed class LexerException : TranslatorException
    {
        public LexerException(string message) : base(message, null) { }
        public LexerException(string message, SourceContext context) : base(message, context) { }
    }
}
