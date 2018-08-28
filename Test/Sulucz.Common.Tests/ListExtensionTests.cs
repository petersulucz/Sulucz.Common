// <copyright file="ListExtensionTests.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common.Tests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The list extensions tests.
    /// </summary>
    [TestClass]
    public class ListExtensionTests
    {
        /// <summary>
        /// Tests a readonly collection.
        /// </summary>
        [TestMethod]
        public void TestAsReadonly()
        {
            var testList = new[] { 1, 2, 3, 4, 5 };
            var @readonlyList = testList.ToReadOnlyCollection();

            var k = 0;
            foreach (var i in readonlyList)
            {
                Assert.AreEqual(testList[k++], i);
            }
        }

        /// <summary>
        /// Test to make sure we dont do a copy if the list is already readonly.
        /// </summary>
        [TestMethod]
        public void TestReadonlyIfCollectionIsAlreadyReadonly()
        {
            var readonlyList = new List<int> { 1, 2, 3, 4, 5 };

            Assert.AreSame(readonlyList, readonlyList.ToReadOnlyCollection());
        }

        /// <summary>
        /// Test to make sure we create a new object if it is not readonly.
        /// </summary>
        [TestMethod]
        public void TestReadonlyWithNonReadonly()
        {
            IEnumerable<int> Items()
            {
                yield return 1;
                yield return 2;
                yield return 3;
                yield return 4;
                yield return 5;
            }

            var items = Items();
            Assert.AreNotSame(items, items.ToReadOnlyCollection());
        }
    }
}
