// <copyright file="Token.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common.Json.Internal
{
    internal class Token
    {
        public Token(string token, TokenType tokenType)
        {
            this.Value = token;
            this.Type = tokenType;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Gets the token type.
        /// </summary>
        public TokenType Type { get; }
    }
}
