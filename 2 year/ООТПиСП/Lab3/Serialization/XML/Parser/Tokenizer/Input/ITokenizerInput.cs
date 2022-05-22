namespace Serialization.XML.Parser
{
    public interface ITokenizerInput
    {
        char ReadChar();
        bool UnreadChar();
        string ReadWhole();

        Context GetCurrentContext();
    }
}