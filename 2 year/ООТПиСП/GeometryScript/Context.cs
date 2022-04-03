namespace GeometryScript.FrontEnd
{
    public sealed class Context
    {
        public string File { get; set; }
        public bool IsInFile { get; set; }

        public int Line { get; set; }
        public int LineOffset { get; set; }

        public static Context Empty
        {
            get
            {
                return new Context()
                {
                    File = null,
                    IsInFile = false,
                    Line = -1,
                    LineOffset = -1
                };
            }
        }

        public override string ToString() =>
            $"line: {this.Line}, char: {this.LineOffset} [{this.File}]";
    }
}
