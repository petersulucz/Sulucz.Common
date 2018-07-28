// <copyright file="UnitTestHelpers.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common.Json.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Sulucz.Common.Json.Internal;

    internal static class UnitTestHelpers
    {
        /// <summary>
        /// Get a lexical analyzer.
        /// </summary>
        /// <param name="testString">The test string.</param>
        /// <returns>The lexican analyzer.</returns>
        public static LexicalAnalyzer GetLex(string testString)
        {
            return new LexicalAnalyzer(new StreamReader(new MemoryStream(Encoding.Unicode.GetBytes(testString)), Encoding.Unicode));
        }

        /// <summary>
        /// Gets streams for the encodings.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The streams.</returns>
        public static IEnumerable<Tuple<Stream, Encoding>> CreateStringStream(string input)
        {
            var encodings = new[] { Encoding.ASCII, Encoding.Unicode, Encoding.UTF8 };

            foreach (var encoding in encodings)
            {
                var memory = new MemoryStream(encoding.GetBytes(input));

                yield return Tuple.Create((Stream)memory, encoding);
            }
        }
    }
}
