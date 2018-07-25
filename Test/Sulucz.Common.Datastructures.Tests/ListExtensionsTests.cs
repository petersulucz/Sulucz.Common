// <copyright file="ListExtensionsTests.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common.Datastructures.Tests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Sulucz.Common.Datastructures.Extensions;

    [TestClass]
    public class ListExtensionsTests
    {
        /// <summary>
        /// Test shuffle with an empty list.
        /// </summary>
        [TestMethod]
        public void TestListShuffleEmpty()
        {
            var result = Enumerable.Empty<object>().Shuffle();
            Assert.IsFalse(result.Any());
        }

        /// <summary>
        /// Tests shuffling a list.
        /// </summary>
        [TestMethod]
        public void TestListShuffle()
        {
            var set = Enumerable.Range(0, 100);

            var isNotInOrder = false;
            var count = 0;

            foreach (var index in set.Shuffle())
            {
                if (count++ != index)
                {
                    isNotInOrder = true;
                }
            }

            Assert.IsTrue(isNotInOrder);
        }
    }
}
