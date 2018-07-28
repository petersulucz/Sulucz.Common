// <copyright file="ObjectParser.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common.Json.Internal
{
    using System.Collections.Generic;
    using System.Dynamic;

    internal static class ObjectParser
    {
        public static bool TryParseObject(Token current, LexicalAnalyzer lex, out dynamic result)
        {
            var obj = new ExpandoObject() as IDictionary<string, object>;

            while (lex.TryGetNextToken(out var token))
            {
                switch (token.Type)
                {
                    case TokenType.ObjectClose:
                        goto success;

                    case TokenType.String:
                        {
                            if (false == PairParser.TryParsePair(token.Value, lex, out var parsed))
                            {
                                goto exit;
                            }

                            obj[parsed.Key] = parsed.Value;
                            break;
                        }

                    case TokenType.Comma:
                        continue;
                }
            }

            success:
            result = obj;
            return true;

            exit:
            result = null;
            return false;
        }
    }
}
