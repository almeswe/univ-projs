namespace GeometryScript.FrontEnd.Input
{
    public interface ITokenizerInput
    {
        char ReadChar();
        bool UnreadChar();
        string ReadWhole();
    
        Context GetCurrentContext();
    }
}