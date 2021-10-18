using Translator.JsTranslator.Lexer;
using Translator.JsTranslator.Lexer.Input;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TranslatorDiagnostics.LexerDiagnostics.RecognitionTest
{
    [TestClass]
    public sealed class RecognitionTest
    {
        [TestMethod]
        public void CharsRecognitionTest()
        {
            var lexer = new Lexer(new FileInput("../../LexerDiagnostics/RecognitionTest/Chars.txt"));
            var tokens = lexer.Tokenize();

            foreach (var token in tokens)
                Assert.AreEqual(TokenKind.TokenChar, token.Kind);

            Assert.AreEqual('a', tokens[0].Lexeme[0]);
            Assert.AreEqual('b', tokens[1].Lexeme[0]);
            Assert.AreEqual('c', tokens[2].Lexeme[0]);
            Assert.AreEqual('1', tokens[3].Lexeme[0]);
            Assert.AreEqual('2', tokens[4].Lexeme[0]);
            Assert.AreEqual('3', tokens[5].Lexeme[0]);
            Assert.AreEqual('\n', tokens[6].Lexeme[0]);
            Assert.AreEqual('\r', tokens[7].Lexeme[0]);
            Assert.AreEqual('\\', tokens[8].Lexeme[0]);
            Assert.AreEqual('\'', tokens[9].Lexeme[0]);
            Assert.AreEqual('\"', tokens[10].Lexeme[0]);
            Assert.AreEqual('\a', tokens[11].Lexeme[0]);
            Assert.AreEqual('\b', tokens[12].Lexeme[0]);
            Assert.AreEqual('\f', tokens[13].Lexeme[0]);
            Assert.AreEqual('\t', tokens[14].Lexeme[0]);
            Assert.AreEqual('\0', tokens[15].Lexeme[0]);
            Assert.AreEqual('\v', tokens[16].Lexeme[0]);
        }
    
        [TestMethod]
        public void StringsRecognitionTest()
        {
            var lexer = new Lexer(new FileInput("../../LexerDiagnostics/RecognitionTest/Strings.txt"));
            var tokens = lexer.Tokenize();

            foreach (var token in tokens)
                Assert.AreEqual(TokenKind.TokenString, token.Kind);

            Assert.AreEqual("hello world!", tokens[0].Lexeme);
            Assert.AreEqual("!@#$%^&*(){}[]~`/*-+?.,:;", tokens[1].Lexeme);
            Assert.AreEqual("newln->\n<-", tokens[2].Lexeme);
            Assert.AreEqual("caret->\r<-", tokens[3].Lexeme);
            Assert.AreEqual("term->\0<-", tokens[4].Lexeme);
            Assert.AreEqual("a->\b<-", tokens[5].Lexeme);
            Assert.AreEqual("b->\b<-", tokens[6].Lexeme);
            Assert.AreEqual("f->\f<-", tokens[7].Lexeme);
            Assert.AreEqual(">\"\'<", tokens[8].Lexeme);
        }
    
        [TestMethod]
        public void NumbersRecognitionTest()
        {
            var lexer = new Lexer(new FileInput("../../LexerDiagnostics/RecognitionTest/Numbers.txt"));
            var tokens = lexer.Tokenize();

            for (int i = 0; i < tokens.Count; i++)
            {
                var kind = i < 2 ? TokenKind.TokenNumber : TokenKind.TokenFNumber;
                Assert.AreEqual(kind, tokens[i].Kind);
            }

            Assert.AreEqual("001", tokens[0].Lexeme);
            Assert.AreEqual("12333", tokens[1].Lexeme);
            Assert.AreEqual("1.1", tokens[2].Lexeme);
            Assert.AreEqual("0.1", tokens[3].Lexeme);
            Assert.AreEqual("123.123", tokens[4].Lexeme);
            Assert.AreEqual("1.123123123123", tokens[5].Lexeme);
        }
    }
}
