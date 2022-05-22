using System;

namespace Serialization.XML.Parser.Exceptions
{
    public sealed class XMLParserException : Exception
    {
        public XMLParserException(string message) 
            : base(message) { }
    }
}
