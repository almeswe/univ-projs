namespace GeometryScript.FrontEnd.Tokenizer
{
    public sealed class Token
    {
        public string Lexeme { get; private set; }
        public TokenKind Kind { get; private set; }
        public Context Context { get; private set; }

        public Token(string lexeme, TokenKind kind) 
            : this(lexeme, kind, Context.Empty) 
        { }

        public Token(string lexeme, TokenKind kind, Context context)
        {
            this.Kind = kind;
            this.Lexeme = lexeme;
            this.Context = context;
        }
    }
}
