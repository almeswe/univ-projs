using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.JsTranslator.Lexer
{
    public enum TokenKind
    {
        TokenKeywordAwait, //Keywords
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
        TokenKeywordPublick,
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

        TokenOpParen, //Keychars
        TokenClParen,
        TokenOpBracket,
        TokenClBracket,
        TokenOpBrace,
        TokenClBrace,
        TokenQuestion,
        TokenColon,
        TokenSemicolon,
        TokenDot,
        TokenSQuote,
        TokenDQuote,

        TokenPlus, // ARITH
        TokenDash,
        TokenAsterisk,
        TokenSlash,
        TokenModulus,
        TokenInc,
        TokenDec,

        TokenLgEq, // LOGICAL
        TokenLgTypeEq, 
        TokenLgNeq,
        Token, // "<"
        Token, // ">"
        TokenGreaterEqThan,
        TokenLessEqThan,
        TokenLgEnd,
        TokenLgOr,
        TokenLgNot, 

        TokenAssign, //ASSIGN
        TokenAddAssign,
        TokenSubAssign,
        TokenMulAssign,
        TokenDivAssign,
        TokenModAssign,
        TokenAmpersand,
        TokenBar,
        TokenCaret,
        TokenTilde,
        TokenLShift,
        TokenRShift,
        Token, // ">>>"
    }

    public sealed class Token
    {
        public TokenKind Kind { get; private set; }   
        public Token()
        {
            
        }
    }
}
