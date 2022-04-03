using System.Collections.Generic;

namespace GeometryScript.FrontEnd.Script
{
    public sealed class ScriptLineOption
    {
        public ScriptLineArgument Argument { get; private set; }
        public IEnumerable<ScriptLineValue> Values { get; private set; }

        public ScriptLineOption(ScriptLineArgument argument,
            IEnumerable<ScriptLineValue> values)
        {
            this.Values = values;
            this.Argument = argument;
        }
    }
}