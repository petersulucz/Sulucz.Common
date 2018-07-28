// <copyright file="BooleanParser.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common.Json.Internal
{
    /// <summary>
    /// The boolean parser.
    /// </summary>
    internal static class BooleanParser
    {
        /// <summary>
        /// Parse a boolean.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>True on success, false on failure.</returns>
        public static bool TryParseBool(string key, out bool value)
        {
            return bool.TryParse(key, out value);
        }
    }
}
