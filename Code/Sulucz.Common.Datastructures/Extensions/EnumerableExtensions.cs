// <copyright file="EnumerableExtensions.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common.Datastructures.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Enumerable extensions.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Shuffle a list.
        /// </summary>
        /// <typeparam name="T">The type of the enumerable.</typeparam>
        /// <param name="self">The enumerable.</param>
        /// <returns>The list.</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> self)
        {
            var random = RandomExtensions.Current;
            var holdingList = self.ToList();

            while (true == holdingList.Any())
            {
                var index = random.Next(0, holdingList.Count);
                yield return holdingList[index];
                holdingList.RemoveAt(index);
            }

            yield break;
        }
    }
}
