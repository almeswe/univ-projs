namespace Serialization.XML
{
    public sealed class XMLProperty
    {
        public string Name { get; private set; }
        public string Value { get; private set; }

        public XMLProperty(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public override string ToString() =>
            $"{Name}=\"{Value}\"";
    }
}