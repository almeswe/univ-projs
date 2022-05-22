using System;

namespace Serialization.XMLSerialization
{
    public sealed class XMLSerializerException : Exception
    {
        public XMLSerializerException(string message) : base(message)
        { }
    }
}
