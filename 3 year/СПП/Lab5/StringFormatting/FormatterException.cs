using System;

namespace StringFormatting
{
    public class FormatterException : Exception
    {
        public FormatterException(string message) 
            : base(message) { }
    }
}
