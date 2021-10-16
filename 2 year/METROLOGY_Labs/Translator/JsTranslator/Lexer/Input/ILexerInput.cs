namespace Translator.JsTranslator.Lexer.Input
{
    public interface ILexerInput
    {
        bool IsFile { get; }
        string File { get; }

        bool Soi();
        bool Eoi();
        char PeekChar();
        char GetNextChar();
        char GetCurrentChar();
        char UngetCurrentChar();
    }
}