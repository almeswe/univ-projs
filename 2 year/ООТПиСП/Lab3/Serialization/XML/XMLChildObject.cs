using System;
using System.Collections.Generic;

namespace Serialization.XML
{
    public sealed class XMLChildObject
    {
        public string Tag { get; private set; }
        public XMLValue Value { get; set; }
        public List<XMLProperty> Properties { get; private set; }

        public XMLChildObject(string tag) : 
            this(tag, new List<XMLProperty>(), null)
        { }

        public XMLChildObject(string tag, List<XMLProperty> properties) :
            this(tag, properties, null)
        { }

        public XMLChildObject(string tag, List<XMLProperty> properties, XMLValue value)
        {
            this.Tag = tag;
            this.Value = value;
            this.Properties = properties;
        }

        public override string ToString() =>
            this.ToString("");

        public string ToString(string indent = "")
        {
            var properties = this.Properties.Count > 0 ? 
                " " : string.Empty;
            var internalIndent = $"{indent}   ";
            foreach (var property in this.Properties)
                properties += property.ToString();
            var result = $"{indent}<{this.Tag}{properties}>";
            if (this.Value is XMLValueImmediate)
            {
                return $"{result}\"{(this.Value as XMLValueImmediate).Value}" +
                    $"\"</{this.Tag}>\r\n";
            }
            else if (this.Value is XMLValueChilds)
            {
                result += "\r\n";
                foreach (var child in (this.Value as XMLValueChilds).Childs)
                    result += child.ToString(internalIndent);
                return $"{result}{indent}</{this.Tag}>\r\n";
            }
            return $"{result}</{this.Tag}>\r\n";
        }

        public void Print(string indent = "")
        {
            var properties = " ";
            var internalIndent = $"{indent}   ";
            foreach (var property in this.Properties)
                properties += property.ToString();
            Console.WriteLine($"{indent}<{this.Tag} {properties}>");
            if (this.Value is XMLValueImmediate)
                Console.WriteLine($"{internalIndent}\"" +
                    $"{(this.Value as XMLValueImmediate).Value}\"");
            else if (this.Value is XMLValueChilds)
            {
                foreach (var child in (this.Value as XMLValueChilds).Childs)
                    child.Print(internalIndent);
            }
            Console.WriteLine($"{indent}</{this.Tag}>");
        }
    }
}