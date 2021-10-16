using System;

namespace Translator.JsTranslator.SpecificCharExtensions
{
    public static class CharExtensions
    {
        public static bool IsDigit(this char ch) =>
            char.IsDigit(ch);
        public static bool IsForNum(this char ch) =>
            char.IsDigit(ch) || ch == '.';
        public static bool IsForIdnt(this char ch) =>
            char.IsLetter(ch) || ch == '_';
        public static bool IsForIdntInt(this char ch) =>
            char.IsLetter(ch) || char.IsDigit(ch) || ch == '_';
        public static bool Is(this char ch, params char[] chars)
        {
            foreach (char c in chars)
                if (c == ch)
                    return true;
            return false;
        }
    }
}