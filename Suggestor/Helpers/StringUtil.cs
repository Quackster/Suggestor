using System;

namespace Suggestor.Helpers
{
    static class StringUtil
    {

        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input[0].ToString().ToUpper() + input.Substring(1);
            }
        }

        public static string ExtractNonAlphanumeric(this string input)
        {
            string nonAlphanumeric = "";
            int i = input.Length - 1;

            while (i >= 0 && !char.IsLetterOrDigit(input[i]))
            {
                nonAlphanumeric = input[i] + nonAlphanumeric;
                i--;
            }

            return nonAlphanumeric;
        }
    }
}