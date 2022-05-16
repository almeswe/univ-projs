using System.Collections.Generic;

namespace Serialization.XML
{
    public sealed class XMLRootObject
    {
        public List<XMLChildObject> Childs { get; private set; }

        private XMLRootObject() =>
            this.Childs = new List<XMLChildObject>();
        
        public static XMLRootObject Empty() =>
            new XMLRootObject();

        public override string ToString()
        {
            string result = string.Empty;
            foreach (var child in this.Childs)
                result += child.ToString();
            return result;
        }
    }
}