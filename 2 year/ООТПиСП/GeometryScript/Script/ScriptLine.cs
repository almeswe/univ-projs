using System.Collections.Generic;

namespace GeometryScript.FrontEnd.Script
{
    public sealed class ScriptLine
    {
        public string Shape { get; private set; }
        public IEnumerable<ScriptLineOption> Options { get; private set; }

        public ScriptLine(string shape, IEnumerable<ScriptLineOption> options)
        {
            this.Shape = shape;
            this.Options = options;
        }

        public Dictionary<string, List<int>> Dump()
        {
            var dump = new Dictionary<string, List<int>>();
            foreach (var option in this.Options)
            {
                var values = new List<int>();
                foreach (var value in option.Values)
                    values.Add(value.Value);
                dump.Add(option.Argument.Argument, values);
            }
            return dump;
        }
    }
}