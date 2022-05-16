namespace Serialization.XML
{
    public sealed class XMLValueChilds : XMLValue
    {
        public XMLChildObject[] Childs { get; private set; }

        public XMLValueChilds(XMLChildObject[] childs) =>
            this.Childs = childs;
    }
}
