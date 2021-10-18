using Translator.JsTranslator;
using Translator.JsTranslator.Lexer;
using Translator.JsTranslator.Lexer.Input;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TranslatorDiagnostics.LexerDiagnostics.ContextTest
{
    [TestClass]
    public sealed class ContextTest
    {
        private string _file;
        
        [TestMethod]
        public void TestContextRecognition()
        {
            var lexer = new Lexer(new FileInput(this._file = "../../LexerDiagnostics/ContextTest/Sample.txt"));
            var tokens = lexer.Tokenize();

            this.AssertContexts(this.Build(6, 25, 3), tokens[51].Context);
            this.AssertContexts(this.Build(5, 53, 7), tokens[44].Context);
            this.AssertContexts(this.Build(12, 31, 11), tokens[80].Context);
            this.AssertContexts(this.Build(2, 21, 1), tokens[12].Context);
            this.AssertContexts(this.Build(8, 32, 1), tokens[63].Context);
        }

        public SourceContext Build(uint line, uint start, uint size) =>
            new SourceContext(line, size, start, this._file);
    
        public void AssertContexts(SourceContext s1, SourceContext s2)
        {
            Assert.AreEqual(s1.Line, s2.Line);
            Assert.AreEqual(s1.Start, s2.Start);
            Assert.AreEqual(s1.Size, s2.Size);
            Assert.AreEqual(s1.File, s2.File);
        }
    }
}