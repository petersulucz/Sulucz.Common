// <copyright file="TokenType.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common.Json.Internal
{
    internal enum TokenType
    {
        ObjectOpen,
        ObjectClose,
        ArrayOpen,
        ArrayClose,
        Comma,
        Colon,
        String,
        Number,
        Boolean
    }
}
