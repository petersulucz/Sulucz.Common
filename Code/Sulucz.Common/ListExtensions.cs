// <copyright file="ListExtensions.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// List extensions.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Converts this enumerable to a readonly collection.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="self">The enumerable.</param>
        /// <returns>The readonly collection.</returns>
        public static IReadOnlyCollection<T> ToReadonlyCollection<T>(this IEnumerable<T> self)
        {
            if (self is IReadOnlyCollection<T> already)
            {
                return already;
            }

            return new ReadOnlyCollection<T>(self.ToList());
        }
    }
}
