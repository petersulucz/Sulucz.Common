// <copyright file="LexicalAnalyzer.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common.Json.Internal
{
    using System;
    using System.IO;
    using System.Text;

    internal class LexicalAnalyzer
    {
        private readonly StreamReader reader;
        private readonly StringBuilder builder;

        public LexicalAnalyzer(StreamReader reader)
        {
            this.reader = reader;
            this.builder = new StringBuilder();
        }

        /// <summary>
        /// Try to get the next token.
        /// </summary>
        /// <param name="result">The result string.</param>
        /// <returns>True if there is another token.</returns>
        public bool TryGetNextToken(out Token result)
        {
            char current = default(char);
            this.builder.Clear();

            // Eat all of the white space.
            LexicalAnalyzer.AdvanceWhile(this.reader, c => char.IsWhiteSpace(c));

            current = (char)this.reader.Read();

            switch (current)
            {
                case '{':
                    this.builder.Append(current);
                    result = new Token(this.builder.ToString(), TokenType.ObjectOpen);
                    return true;
                case '}':
                    this.builder.Append(current);
                    result = new Token(this.builder.ToString(), TokenType.ObjectClose);
                    return true;
                case ':':
                    this.builder.Append(current);
                    result = new Token(this.builder.ToString(), TokenType.Colon);
                    return true;
                case ',':
                    this.builder.Append(current);
                    result = new Token(this.builder.ToString(), TokenType.Comma);
                    return true;
                case '[':
                    this.builder.Append(current);
                    result = new Token(this.builder.ToString(), TokenType.ArrayOpen);
                    return true;
                case ']':
                    this.builder.Append(current);
                    result = new Token(this.builder.ToString(), TokenType.ArrayClose);
                    return true;
                case '"':
                    LexicalAnalyzer.ReadStringToken(this.reader, this.builder);
                    result = new Token(this.builder.ToString(), TokenType.String);
                    return true;
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '-':
                    if (false == LexicalAnalyzer.ReadLiteral(current, this.reader, this.builder))
                    {
                        throw new Exception();
                    }

                    result = new Token(this.builder.ToString(), TokenType.Number);
                    return true;

                case 't':
                case 'f':
                case 'T':
                case 'F':
                    if (false == LexicalAnalyzer.ReadLiteral(current, this.reader, this.builder))
                    {
                        throw new Exception();
                    }

                    result = new Token(this.builder.ToString(), TokenType.Boolean);
                    return true;
            }

            result = null;
            return false;
        }

        /// <summary>
        /// Read a string token.
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="builder">The string builder.</param>
        /// <returns>True on success.</returns>
        internal static bool ReadStringToken(StreamReader reader, StringBuilder builder)
        {
            const char CloseChar = '"';

            var previous = default(char);
            var current = (char)reader.Read();
            while (false == reader.EndOfStream && false == (current == CloseChar && previous != '\\'))
            {
                builder.Append(current);

                previous = current;
                current = (char)reader.Read();
            }

            if (CloseChar == current)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Read a literal from the reader.
        /// </summary>
        /// <param name="current">The currently read char.</param>
        /// <param name="reader">The reader.</param>
        /// <param name="builder">The builder.</param>
        /// <returns>True on success. False otherwise.</returns>
        internal static bool ReadLiteral(char current, StreamReader reader, StringBuilder builder)
        {
            while (true)
            {
                builder.Append(current);
                if (true == reader.EndOfStream)
                {
                    break;
                }

                current = (char)reader.Peek();

                if (false == char.IsLetterOrDigit(current) && current != '.')
                {
                    break;
                }

                reader.Read();
            }

            return true;
        }

        /// <summary>
        /// Advance until checker returns false.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="checker">The checker.</param>
        private static void AdvanceWhile(StreamReader stream, Func<char, bool> checker)
        {
            while (false == stream.EndOfStream)
            {
                var current = (char)stream.Peek();

                if (false == checker(current))
                {
                    return;
                }
                else
                {
                    stream.Read();
                }
            }
        }
    }
}
