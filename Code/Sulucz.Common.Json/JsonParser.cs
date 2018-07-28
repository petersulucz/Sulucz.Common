// <copyright file="JsonParser.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common.Json
{
    using System.IO;
    using System.Text;
    using Sulucz.Common.Json.Internal;

    public class JsonParser
    {
        private readonly Stream stream;
        private readonly StreamReader reader;

        public JsonParser(Stream stream, Encoding encoding)
        {
            this.stream = stream;
            this.reader = new StreamReader(this.stream, encoding);
        }

        public bool TryParse(out dynamic result)
        {
            var lex = new LexicalAnalyzer(this.reader);

            if (false == lex.TryGetNextToken(out var token))
            {
                goto failed;
            }

            if (TokenType.ArrayOpen == token.Type)
            {
                var b = ArrayParser.TryParseArray(token, lex, out var resArray);
                result = resArray;
                return b;
            }

            if (TokenType.ObjectOpen == token.Type)
            {
                return ObjectParser.TryParseObject(token, lex, out result);
            }

            failed:
            result = null;
            return false;
        }
    }
}
