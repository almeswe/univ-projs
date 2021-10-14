namespace Translator.JsTranslator.Lexer.Input
{
    public interface ILexerInput
    {
        bool Eoi();
        char PeekChar();
        char GetNextChar();
        char GetCurrentChar();
        char UngetCurrentChar();
    }
}