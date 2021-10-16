namespace Translator.JsTranslator.Lexer
{
    public enum TokenKind
    {
        TokenPlus,
        TokenDash,
        TokenAsterisk,
        TokenSlash,
        TokenModulus,
        TokenOpParen,
        TokenClParen,
        TokenOpBracket,
        TokenClBracket,
        TokenOpBrace,
        TokenClBrace,
        TokenQuestion,
        TokenColon,
        TokenSemicolon,
        TokenDot,
        TokenExlMark,
        TokenLAngle,
        TokenRAngle,
        TokenBar,
        TokenAmpersand,
        TokenCaret,
        TokenTilde,
        TokenAssign,
        TokenSQuote,
        TokenDQuote,

        TokenChar,
        TokenString,
        TokenNumber,
        TokenFNumber,
        TokenIdentifier,

        TokenInc,
        TokenDec,
        TokenLShift,
        TokenRShift,
        TokenURshift,

        TokenLgEq,
        TokenLgNeq,
        TokenLgGEqT,
        TokenLgLEqT,
        TokenLgAnd,
        TokenLgOr,
        TokenLgTypeEq,

        TokenAddAssign,
        TokenSubAssign,
        TokenMulAssign,
        TokenDivAssign,
        TokenModAssign,

        TokenKeywordAwait,
        TokenKeywordBreak,
        TokenKeywordCase,
        TokenKeywordCatch,
        TokenKeywordClass,
        TokenKeywordConst,
        TokenKeywordContinue,
        TokenKeywordDebugger,
        TokenKeywordDefault,
        TokenKeywordDelete,
        TokenKeywordDo,
        TokenKeywordElse,
        TokenKeywordEnum,
        TokenKeywordExport,
        TokenKeywordExtends,
        TokenKeywordFalse,
        TokenKeywordFinally,
        TokenKeywordFor,
        TokenKeywordFunction,
        TokenKeywordIf,
        TokenKeywordImplements,
        TokenKeywordImport,
        TokenKeywordIn,
        TokenKeywordInstanceof,
        TokenKeywordInterface,
        TokenKeywordLet,
        TokenKeywordNew,
        TokenKeywordNull,
        TokenKeywordPackage,
        TokenKeywordPrivate,
        TokenKeywordProtected,
        TokenKeywordPublic,
        TokenKeywordReturn,
        TokenKeywordSuper,
        TokenKeywordSwitch,
        TokenKeywordStatic,
        TokenKeywordThis,
        TokenKeywordThrow,
        TokenKeywordTry,
        TokenKeywordTrue,
        TokenKeywordTypeof,
        TokenKeywordVar,
        TokenKeywordVoid,
        TokenKeywordWhile,
        TokenKeywordWith,
        TokenKeywordYield,
        TokenEOF
    }

    public sealed class Token
    {
        public string Lexeme { get; private set; }

        public TokenKind Kind { get; private set; }
        public SourceContext Context { get; private set; }

        public Token(TokenKind kind, string lexeme, SourceContext context)
        {
            this.Kind = kind;
            this.Lexeme = lexeme;
            this.Context = context;
        }

        public override string ToString() =>
            $"{this.Kind}: {this.Lexeme} {this.Context}";
    }
}