// <copyright file="JsonPair.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common.Json.Internal
{
    internal class JsonPair<T>
    {
        public JsonPair(string key, T value)
        {
            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value { get; }
    }
}
