using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Translator.JsTranslator.Lexer.Input;

namespace Translator.JsTranslator.Lexer
{
    public sealed class Lexer
    {
        private string[] _keywords =
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
        private string[] _keychars =
        {
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
            "\'",
            "\"",
            
            "+", //ARITH
            "-",
            "*",
            "/",
            "%",
            "++",
            "--",

            "==", //LOGICAL
            "===",
            "!=",
            "<",
            ">",
            ">=",
            "<=",
            "&&",
            "||",
            "!",

            "=", //ASSIGN
            "+=",
            "-=",
            "*=",
            "/=",
            "%=",

            "&", //BITWISE
            "|",
            "^",
            "~",
            "<<",
            ">>",
            ">>>"
        };

        public Lexer(ILexerInput input)
        {

        }
    }
}
