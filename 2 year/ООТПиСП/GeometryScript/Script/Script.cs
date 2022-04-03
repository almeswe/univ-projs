using System.Drawing;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;
using Geometry;

namespace GeometryScript.FrontEnd.Script
{
    public sealed class Script
    {
        public IEnumerable<ScriptLine> Lines { get; private set; }

        public Script(IEnumerable<ScriptLine> lines) =>
            this.Lines = lines;

        public void RenderOn(Graphics graphics)
        {
            var shapesAsm = typeof(Shape).GetTypeInfo().Assembly;
            foreach (var line in this.Lines)
            {
                var shapeType = shapesAsm.GetTypes().FirstOrDefault(
                    t => t.Name.ToLower() == line.Shape.ToLower());
                if (shapeType == null)
                    throw new System.ArgumentException(
                        $"Unknown shape type: {line.Shape}");
                try
                {
                    var constructor = shapeType.GetConstructor(new Type[]
                        { typeof(Dictionary<string, List<int>>) });
                    if (constructor != null)
                        (constructor.Invoke(new object[] { line.Dump() }) as Shape)
                            .Draw(graphics);       
                }
                catch (Exception e)
                {
                    throw new System.ArgumentException($"Error occured: {e.Message}." +
                        $"shape: {line.Shape}");
                }
            }
        }
    }
}