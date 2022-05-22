namespace DIContainerTest
{
    public sealed class BoldText : IText
    {
        public IFont _font;

        public BoldText(IFont font) =>
            this._font = font;

        public string TextInfo() =>
            $"Bold Text [{this._font.FontInfo()}]";
    }
}
