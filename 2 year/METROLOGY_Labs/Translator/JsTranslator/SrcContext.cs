namespace Translator.JsTranslator
{
    public sealed class SourceContext
    {
        public uint Line { get; private set; }
        public uint Size { get; private set; }
        public uint Start { get; private set; }

        public string File { get; private set; }

        public SourceContext(uint line, uint size, uint start, string file = "undefined")
        {
            this.Line = line;
            this.Size = size;
            this.Start = start;
            this.File = file;
        }

        public override string ToString() =>
            $"(line: {this.Line}, position: {this.Start}" + 
                (this.File.ToLower() == "undefined" ? ")" : $", file: \'{this.File}\')");

        /*protected override SourceContext operator +(SourceContext starts, SourceContext ends)
        {
        }*/
    }
}
