using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Translator.JsTranslator.Lexer.Input;

namespace Translator
{
    class Program
    {
        static void Main(string[] args)
        {
            //var input = new ConsoleInput(Console.ReadLine());
            var input = new FileInput(@"C:\Users\HP\Desktop\123.txt");
            while (true)
            {
                Console.Write(">");
                switch (Console.ReadLine())
                {
                    case "p":
                        Console.WriteLine(input.PeekChar()); break;
                    case "u":
                        Console.WriteLine(input.UngetCurrentChar()); break;
                    case "gn":
                        Console.WriteLine(input.GetNextChar()); break;
                    case "gc":
                        Console.WriteLine(input.GetCurrentChar()); break;
                }
            }
        }
    }
}
