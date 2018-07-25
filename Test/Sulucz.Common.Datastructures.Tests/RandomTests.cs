// <copyright file="RandomTests.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common.Datastructures.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Sulucz.Common.Datastructures.Extensions;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Tests for the random class.
    /// </summary>
    [TestClass]
    public class RandomTests
    {
        /// <summary>
        /// Tests for the same thread.
        /// </summary>
        [TestMethod]
        public void TestRandomSameThread()
        {
            var rand1 = RandomExtensions.Current;
            var rand2 = RandomExtensions.Current;

            Assert.ReferenceEquals(rand1, rand2);
        }

        /// <summary>
        /// Tests for two differnet threads.
        /// </summary>
        /// <returns>An async task.</returns>
        [TestMethod]
        public async Task TestRandomDiffernetThread()
        {
            Random rand1 = null;
            Random rand2 = null;

            var gate = new Barrier(2);

            var task1 = Task.Run(() =>
            {
                gate.SignalAndWait();
                rand1 = RandomExtensions.Current;
            });
            var task2 = Task.Run(() =>
            {
                gate.SignalAndWait();
                rand2 = RandomExtensions.Current;
            });

            await Task.WhenAll(task1, task2);
            Assert.AreNotSame(rand1, rand2);
        }
    }
}
