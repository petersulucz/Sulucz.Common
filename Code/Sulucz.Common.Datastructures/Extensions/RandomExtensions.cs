// <copyright file="RandomExtensions.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common.Datastructures.Extensions
{
    using System;
    using System.Threading;

    /// <summary>
    /// Random extensions.
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// The thread local random.
        /// </summary>
        [ThreadStatic]
        private static Random threadSafeRandom = null;

        /// <summary>
        /// Gets the current thread's random.
        /// </summary>
        public static Random Current => RandomExtensions.threadSafeRandom ?? (RandomExtensions.threadSafeRandom = new Random());
    }
}
