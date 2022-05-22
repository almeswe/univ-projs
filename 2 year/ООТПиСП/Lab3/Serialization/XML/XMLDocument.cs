using System.Text;

namespace Serialization.XML
{
    public sealed class XMLDocument
    {
        public string Version { get; private set; }
        public Encoding  Encoding { get; private set; }

        public XMLRootObject Root { get; private set; }

        public XMLDocument() : this("1.1", Encoding.UTF8) 
        { }

        public XMLDocument(string version, Encoding encoding)
        {
            this.Version = version;
            this.Encoding = encoding;
            this.Root = XMLRootObject.Empty();
        }

        public override string ToString()
        {
            var result = $"<?xml encoding=\"{this.Encoding.HeaderName}\"" +
                $" version=\"{this.Version}\"?>\r\n";
            return $"{result}{this.Root.ToString()}";
        }
    }
}