namespace GeometryScript.FrontEnd.Script
{
    public sealed class ScriptLineValue
    {
        public int Value { get; private set; }

        public ScriptLineValue(int value) =>
            this.Value = value;
    }
}