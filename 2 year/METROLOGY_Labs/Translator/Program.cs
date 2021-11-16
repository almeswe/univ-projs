using System;

using Translator.JsTranslator.Lexer;
using Translator.JsTranslator.Syntax;
using Translator.JsTranslator.Lexer.Input;

namespace Translator
{
    //https://github.com/chadsmith12/pure-javascript-examples
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var lexer = new Lexer(new FileInput(
                    @"D:\university-projects\univ-projs\2 year\METROLOGY_Labs\Translator\Test.txt"));
                var parser = new Parser(lexer.Tokenize());

                var ast = parser.Parse();
                AstRoot.Print(ast, "", true);
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
