using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Utility functions class.
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Quote in comma separated value string lists.
        /// </summary>
        private const string Quote = "\"";

        /// <summary>
        /// Escaped quote in comma separated value string lists.
        /// </summary>
        private const string EscapedQuote = "\"\"";

        /// <summary>
        /// Characters required to be quoted for comma separated value string lists.
        /// </summary>
        private static char[] charactersRequiredToBeQuoted = { ',', '\"' };

        /// <summary>
        /// Regular expression to split comma separated value string lists.
        /// </summary>
        private static Regex splitterRegex = new Regex(@",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))");

        /// <summary>
        /// Escapes the string list into a comma separated value string.
        /// </summary>
        /// <param name="values">The values to escape.</param>
        /// <returns>The escaped comma separated value string.</returns>
        public static string EscapeList(params string[] values)
        {
            StringBuilder builder = new StringBuilder();

            for (var index = 0; index < values.Length; index++)
            {
                var value = values[index];

                builder.Append('\"' + Escape(value) + '\"');

                if (index < values.Length - 1)
                {
                    builder.Append(',');
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// Unescapes a comma separated value string list.
        /// </summary>
        /// <param name="value">The comma separated value string.</param>
        /// <returns>The unescaped values.</returns>
        public static string[] UnescapeList(string value)
        {
            string[] values = splitterRegex.Split(value);

            for (int i = 0; i < values.Length; i++)
            {
                values[i] = Unescape(values[i]);
            }

            return values;
        }

        /// <summary>
        /// Determines whether a location is within a expected radius.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="expectedLatitude">The expected latitude.</param>
        /// <param name="expectedLongitude">The expected longitude.</param>
        /// <param name="tolerance">The tolerance in decimal degrees.</param>
        /// <returns>
        /// <c>true</c> if location is within the expected radius, otherwise, <c>false</c>.
        /// </returns>
        public static bool IsWithinRange(double latitude, double longitude, double expectedLatitude, double expectedLongitude, double tolerance)
        {
            return GetDelta(latitude, longitude, expectedLatitude, expectedLongitude) <= Math.Abs(tolerance);
        }

        /// <summary>
        /// Gets the absolute delta between to locations.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="expectedLatitude">The expected latitude.</param>
        /// <param name="expectedLongitude">The expected longitude.</param>
        /// <returns>The absolute delta in decimal degrees.</returns>
        public static double GetDelta(double latitude, double longitude, double expectedLatitude, double expectedLongitude)
        {
            return Math.Abs(Math.Pow(Math.Pow(expectedLatitude - latitude, 2) + Math.Pow(expectedLongitude - longitude, 2), 0.5));
        }

        /// <summary>
        /// Escapes the specified string to create comma separated value string list.
        /// </summary>
        /// <param name="value">The string to escape.</param>
        /// <returns>The escaped string.</returns>
        private static string Escape(string value)
        {
            if (value.Contains(Quote))
            {
                value = value.Replace(Quote, EscapedQuote);
            }

            if (value.IndexOfAny(charactersRequiredToBeQuoted) > -1)
            {
                value = Quote + value + Quote;
            }

            return value;
        }

        /// <summary>
        /// Unescapes the specified string coming from create comma separated value string list.
        /// </summary>
        /// <param name="value">The string to unescape.</param>
        /// <returns>The unescaped string.</returns>
        public static string Unescape(string value)
        {
            if (value.StartsWith(Quote) && value.EndsWith(Quote))
            {
                value = value.Substring(1, value.Length - 2);

                if (value.Contains(EscapedQuote))
                {
                    value = value.Replace(EscapedQuote, Quote);
                }
            }

            return value;
        }
        public static string RemoveWhitespace( string input)
        {
            int j = 0, inputlen = input.Length;
            char[] newarr = new char[inputlen];

            for (int i = 0; i < inputlen; ++i)
            {
                char tmp = input[i];

                if (!char.IsWhiteSpace(tmp))
                {
                    newarr[j] = tmp;
                    ++j;
                }
            }

            return new String(newarr, 0, j);
        }
    }
}
