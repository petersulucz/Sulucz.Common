// <copyright file="ArrayParser.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common.Json.Internal
{
    using System.Collections.Generic;

    internal static class ArrayParser
    {
        /// <summary>
        /// Try to parse an array.
        /// </summary>
        /// <param name="initial">The initial value. Should be the array openner.</param>
        /// <param name="lex">The lexical analyzer.</param>
        /// <param name="result">The result.</param>
        /// <returns>True on success. False otherwise.</returns>
        public static bool TryParseArray(Token initial, LexicalAnalyzer lex, out List<dynamic> result)
        {
            var array = new List<dynamic>();

            while (true == lex.TryGetNextToken(out var current))
            {
                switch (current.Type)
                {
                    case TokenType.ArrayClose:
                        {
                            result = array;
                            return true;
                        }

                    case TokenType.ObjectOpen:
                        {
                            if (false == ObjectParser.TryParseObject(current, lex, out var objResult))
                            {
                                goto failed;
                            }

                            array.Add(objResult);
                            break;
                        }

                    case TokenType.ArrayOpen:
                        {
                            if (false == ArrayParser.TryParseArray(current, lex, out var objResult))
                            {
                                goto failed;
                            }

                            array.Add(objResult);
                            break;
                        }

                    case TokenType.Boolean:
                        {
                            if (false == BooleanParser.TryParseBool(current.Value, out var boolResult))
                            {
                                goto failed;
                            }

                            array.Add(boolResult);
                            break;
                        }

                    case TokenType.String:
                        {
                            array.Add(current.Value);
                            break;
                        }

                    case TokenType.Number:
                        {
                            if (false == NumberParser.ParseNumber(current.Value, out var numResult))
                            {
                                goto failed;
                            }

                            array.Add(numResult);
                            break;
                        }

                    case TokenType.Comma:
                        break;

                    default:
                        goto failed;
                }
            }

            failed:
            result = null;
            return false;
        }
    }
}
