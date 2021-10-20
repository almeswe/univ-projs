using Translator.JsTranslator.Lexer;
using Translator.JsTranslator.Lexer.Input;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TranslatorDiagnostics.LexerDiagnostics
{
    [TestClass]
    public sealed class LexemeTest
    {
        [TestMethod]
        public void TestLexemes()
        {
            var lexer = new Lexer(new FileInput("../../LexerDiagnostics/LexemeTest/Lexemes.txt"));
            var tokens = lexer.Tokenize();

            Assert.AreEqual(TokenKind.TokenPlus, tokens[0].Kind);
            Assert.AreEqual(TokenKind.TokenDash, tokens[1].Kind);
            Assert.AreEqual(TokenKind.TokenAsterisk, tokens[2].Kind);
            Assert.AreEqual(TokenKind.TokenSlash, tokens[3].Kind);
            Assert.AreEqual(TokenKind.TokenModulus, tokens[4].Kind);
            Assert.AreEqual(TokenKind.TokenOpParen, tokens[5].Kind);
            Assert.AreEqual(TokenKind.TokenClParen, tokens[6].Kind);
            Assert.AreEqual(TokenKind.TokenOpBracket, tokens[7].Kind);
            Assert.AreEqual(TokenKind.TokenClBracket, tokens[8].Kind);
            Assert.AreEqual(TokenKind.TokenOpBrace, tokens[9].Kind);
            Assert.AreEqual(TokenKind.TokenClBrace, tokens[10].Kind);
            Assert.AreEqual(TokenKind.TokenQuestion, tokens[11].Kind);
            Assert.AreEqual(TokenKind.TokenColon, tokens[12].Kind);
            Assert.AreEqual(TokenKind.TokenSemicolon, tokens[13].Kind);
            Assert.AreEqual(TokenKind.TokenDot, tokens[14].Kind);
            Assert.AreEqual(TokenKind.TokenExlMark, tokens[15].Kind);
            Assert.AreEqual(TokenKind.TokenLAngle, tokens[16].Kind);
            Assert.AreEqual(TokenKind.TokenRAngle, tokens[17].Kind);
            Assert.AreEqual(TokenKind.TokenBar, tokens[18].Kind);
            Assert.AreEqual(TokenKind.TokenAmpersand, tokens[19].Kind);
            Assert.AreEqual(TokenKind.TokenCaret, tokens[20].Kind);
            Assert.AreEqual(TokenKind.TokenTilde, tokens[21].Kind);
            Assert.AreEqual(TokenKind.TokenAssign, tokens[22].Kind);
            Assert.AreEqual(TokenKind.TokenInc, tokens[23].Kind);
            Assert.AreEqual(TokenKind.TokenDec, tokens[24].Kind);
            Assert.AreEqual(TokenKind.TokenPower, tokens[25].Kind);
            Assert.AreEqual(TokenKind.TokenLShift, tokens[26].Kind);
            Assert.AreEqual(TokenKind.TokenRShift, tokens[27].Kind);
            Assert.AreEqual(TokenKind.TokenURshift, tokens[28].Kind);
            Assert.AreEqual(TokenKind.TokenLgEq, tokens[29].Kind);
            Assert.AreEqual(TokenKind.TokenLgNeq, tokens[30].Kind);
            Assert.AreEqual(TokenKind.TokenLgGEqT, tokens[31].Kind);
            Assert.AreEqual(TokenKind.TokenLgLEqT, tokens[32].Kind);
            Assert.AreEqual(TokenKind.TokenLgAnd, tokens[33].Kind);
            Assert.AreEqual(TokenKind.TokenLgOr, tokens[34].Kind);
            Assert.AreEqual(TokenKind.TokenLgTypeEq, tokens[35].Kind);
            Assert.AreEqual(TokenKind.TokenAddAssign, tokens[36].Kind);
            Assert.AreEqual(TokenKind.TokenSubAssign, tokens[37].Kind);
            Assert.AreEqual(TokenKind.TokenMulAssign, tokens[38].Kind);
            Assert.AreEqual(TokenKind.TokenDivAssign, tokens[39].Kind);
            Assert.AreEqual(TokenKind.TokenModAssign, tokens[40].Kind);
            Assert.AreEqual(TokenKind.TokenBwXorAssign, tokens[41].Kind);
            Assert.AreEqual(TokenKind.TokenBwOrAssign, tokens[42].Kind);
            Assert.AreEqual(TokenKind.TokenBwAndAssign, tokens[43].Kind);
            Assert.AreEqual(TokenKind.TokenBwNotAssign, tokens[44].Kind);
            Assert.AreEqual(TokenKind.TokenRShiftAssign, tokens[45].Kind);
            Assert.AreEqual(TokenKind.TokenLShiftAssign, tokens[46].Kind);
            Assert.AreEqual(TokenKind.TokenRUShiftAssign, tokens[47].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordAwait, tokens[48].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordBreak, tokens[49].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordCase, tokens[50].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordCatch, tokens[51].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordClass, tokens[52].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordConst, tokens[53].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordContinue, tokens[54].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordDebugger, tokens[55].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordDefault, tokens[56].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordDelete, tokens[57].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordDo, tokens[58].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordElse, tokens[59].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordEnum, tokens[60].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordExport, tokens[61].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordExtends, tokens[62].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordFalse, tokens[63].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordFinally, tokens[64].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordFor, tokens[65].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordFunction, tokens[66].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordIf, tokens[67].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordImplements, tokens[68].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordImport, tokens[69].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordIn, tokens[70].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordInstanceof, tokens[71].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordInterface, tokens[72].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordLet, tokens[73].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordNew, tokens[74].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordNull, tokens[75].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordPackage, tokens[76].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordPrivate, tokens[77].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordProtected, tokens[78].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordPublic, tokens[79].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordReturn, tokens[80].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordSuper, tokens[81].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordSwitch, tokens[82].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordStatic, tokens[83].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordThis, tokens[84].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordThrow, tokens[85].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordTry, tokens[86].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordTrue, tokens[87].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordTypeof, tokens[88].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordVar, tokens[89].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordVoid, tokens[90].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordWhile, tokens[91].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordWith, tokens[92].Kind);
            Assert.AreEqual(TokenKind.TokenKeywordYield, tokens[93].Kind);
        }
    }
}