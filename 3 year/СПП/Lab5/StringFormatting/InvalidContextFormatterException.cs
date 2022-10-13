namespace StringFormatting
{
    public class InvalidContextFormatterException : FormatterException
    {
        public InvalidContextFormatterException()
            : base($"Format argument is interpreted like collection, but it dont.\n") { }
    }
}
