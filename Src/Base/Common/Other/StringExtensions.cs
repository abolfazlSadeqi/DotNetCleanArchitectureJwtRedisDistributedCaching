using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Other
{
    public static class StringExtensions
    {
        public static string ApplyTrim(this string text) => text.Trim();

        public static bool IsEmpty(this string text) => string.IsNullOrEmpty(text);

        public static StringBuilder BuildString(List<string> listStrings, bool hasWordBreak)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var itemString in listStrings)
            {
                if (hasWordBreak)
                    sb.AppendLine(itemString);
                else
                    sb.Append(itemString).Append('\n');
            }

            return sb;
        }

        public static int GetFirstIndexPositionFromWord(string value, string valueToFind, int indexStart = 0)
        {
            if (value.IndexOf(valueToFind) != -1 && indexStart == 0)
                return value.IndexOf(valueToFind);

            else if (value.IndexOf(valueToFind) != -1 && indexStart > 0)
                return value.IndexOf(valueToFind, indexStart);

            return -1;
        }

        public static int GetLastIndexPositionFromWord(string value, string valueToFind, int indexStart = 0)
        {
            if (value.LastIndexOf(valueToFind) != -1 && indexStart == 0)
                return value.LastIndexOf(valueToFind);

            else if (value.LastIndexOf(valueToFind) != -1 && indexStart > 0)
                return value.LastIndexOf(valueToFind, indexStart);

            return -1;
        }
    }
}
