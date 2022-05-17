using System;

namespace DIContainerTest
{
    public sealed class Writer
    {
        private IPen _pen;
        private IText _text;

        public Writer(IPen pen, IText text)
        {
            this._pen = pen;
            this._text = text;
        }

        public void Write(string message) =>
            Console.WriteLine($"Writes {message} of kind " +
                $"{this._text.TextInfo()} with {this._pen.PenInfo()}");
    }
}
