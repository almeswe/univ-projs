namespace Serialization.XML
{ 
    public sealed class XMLValueImmediate : XMLValue
    {
        public string Value { get; private set; }

        public XMLValueImmediate(string value) =>
            this.Value = value;
    }
}