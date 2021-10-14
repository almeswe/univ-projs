using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.JsTranslator.Lexer
{
    public enum TokenKind
    {
        //...
    }

    public sealed class Token
    {
        public TokenKind Kind { get; private set; }   
        public Token()
        {
            
        }
    }
}
