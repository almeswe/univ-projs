namespace StringFormatting
{
    public sealed class InvalidStringFormatterException : FormatterException
    {
        public InvalidStringFormatterException(string message) 
            : base($"Format string is not valid for formatting.\n{message}") { }
    }
}
