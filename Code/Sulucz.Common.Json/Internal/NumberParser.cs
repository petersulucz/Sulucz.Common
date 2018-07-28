// <copyright file="NumberParser.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common.Json.Internal
{
    /// <summary>
    /// The number parser.
    /// </summary>
    internal static class NumberParser
    {
        /// <summary>
        /// Parse a number.
        /// </summary>
        /// <param name="str">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>True on success. False on failure.</returns>
        public static bool ParseNumber(string str, out double value)
        {
            return double.TryParse(str, out value);
        }
    }
}
