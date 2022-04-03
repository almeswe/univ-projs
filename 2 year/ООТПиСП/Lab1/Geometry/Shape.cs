using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

namespace Geometry
{
    public class Shape
    {
        protected Pen _defaultPen => new Pen(Color.Black);

        public virtual void Draw(Graphics g) =>
            throw new NotImplementedException();

        public virtual void FromDump(Dictionary<string, List<int>> dump) =>
            throw new NotImplementedException();
    }

    public static class ShapeDumpExtension
    {
        public static List<int> GetFieldFromDump(this Shape shape,
            Dictionary<string, List<int>> dump, string field)
        {
            var value = dump?.FirstOrDefault(
                v => v.Key == field);
            return value != null ? 
                value.Value.Value : null;
        }

        public static List<int> GetFieldFromDumpOrThrow(this Shape shape,
            Dictionary<string, List<int>> dump, string field)
        {
            var dumped = shape.GetFieldFromDump(dump, field);
            return dumped != null ? dumped :
                throw new System.ArgumentException(
                    $"Cannot get needed argument: {field}");
        }
    }
}
