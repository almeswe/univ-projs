namespace GeometryScript.FrontEnd.Script
{
    public sealed class ScriptLineArgument
    {
        public string Argument { get; private set; }

        public ScriptLineArgument(string argument) =>
            this.Argument = argument;
    }
}