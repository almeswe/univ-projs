using System;

using Translator.JsTranslator.Lexer;
using Translator.JsTranslator.Lexer.Input;

namespace Translator
{
    //https://github.com/chadsmith12/pure-javascript-examples
    class Program
    {
        static void Main(string[] args)
        {
            var l = new Lexer(new StringInput("+= -= ++ -- === >>> << >> >= <= == != + - * & && |"));
            foreach (var t in l.Tokenize())
                Console.WriteLine(t.ToString());
            Console.ReadLine();
        }
    }
}
