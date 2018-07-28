// <copyright file="PairParser.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common.Json.Internal
{
    internal static class PairParser
    {
        public static bool TryParsePair(string key, LexicalAnalyzer lex, out JsonPair<dynamic> result)
        {
            if (false == lex.TryGetNextToken(out var colon) || colon.Type != TokenType.Colon)
            {
                result = null;
                return false;
            }

            if (false == lex.TryGetNextToken(out var value))
            {
                result = null;
                return false;
            }

            switch (value.Type)
            {
                case TokenType.String:
                    result = new JsonPair<dynamic>(key, value.Value);
                    return true;
                case TokenType.Number:
                    if (false == NumberParser.ParseNumber(value.Value, out var num))
                    {
                        goto exit;
                    }

                    result = new JsonPair<dynamic>(key, num);
                    return true;
                case TokenType.Boolean:
                    if (false == BooleanParser.TryParseBool(value.Value, out var b))
                    {
                        goto exit;
                    }

                    result = new JsonPair<dynamic>(key, b);
                    return true;

                case TokenType.ObjectOpen:
                    {
                        if (false == ObjectParser.TryParseObject(value, lex, out var nextObj))
                        {
                            goto exit;
                        }

                        result = new JsonPair<dynamic>(key, nextObj);
                        return true;
                    }

                case TokenType.ArrayOpen:
                    {
                        if (false == ArrayParser.TryParseArray(value, lex, out var nextObj))
                        {
                            goto exit;
                        }

                        result = new JsonPair<dynamic>(key, nextObj);
                        return true;
                    }

                default:
                    result = null;
                    return false;
            }

            exit:
            result = null;
            return false;
        }
    }
}
