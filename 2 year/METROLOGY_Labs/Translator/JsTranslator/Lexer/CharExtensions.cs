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
        public static bool IsSQuote(this char ch) =>
            ch == '\'';
        public static bool IsDQuote(this char ch) =>
            ch == '\"';
        public static bool IsForStr(this char ch) =>
            ch != '\n' && ch != '\"';
        public static bool IsForIdntInt(this char ch) =>
            char.IsLetter(ch) || char.IsDigit(ch) || ch == '_';
        public static int InSequence(this char ch, char[] chars)
        {
            for (int i = 0; i < chars.Length; i++)
                if (chars[i] == ch)
                    return i;
            return -1;
        }
        public static bool Is(this char ch, params char[] chars)
        {
            foreach (char c in chars)
                if (c == ch)
                    return true;
            return false;
        }
    }
}