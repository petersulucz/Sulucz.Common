// <copyright file="ExpandableGridTests.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common.Datastructures.Tests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExpandableGridTests
    {
        [TestMethod]
        public void TestInsert()
        {
            var grid = new ExpandableGrid<object>();
            var obj = new object();
            grid[0, 0] = obj;
            Assert.AreSame(obj, grid[0, 0]);
        }

        [TestMethod]
        public void TestInsert2()
        {
            var grid = new ExpandableGrid<object>();
            var obj = new object();
            grid[-20000, 100] = obj;
            Assert.AreSame(obj, grid[-20000, 100]);
        }

        [TestMethod]
        public void TestMinColumn()
        {
            var grid = new ExpandableGrid<object>();
            var obj = new object();
            grid[0, 0] = obj;
            Assert.AreEqual(0, grid.MinColumn);
        }

        [TestMethod]
        public void TestMinColumn2()
        {
            const int MinColumn = -200;

            var grid = new ExpandableGrid<object>();
            var obj = new object();
            grid[0, MinColumn] = obj;
            Assert.AreEqual(MinColumn, grid.MinColumn);
        }

        [TestMethod]
        public void TestMinColumn3()
        {
            const int MinColumn = -200;

            var grid = new ExpandableGrid<object>();
            var obj = new object();
            grid[0, MinColumn] = obj;
            grid[0, MinColumn + 1] = obj;
            Assert.AreEqual(MinColumn, grid.MinColumn);
        }

        [TestMethod]
        public void TestMaxColumn()
        {
            const int MaxColumn = 200;

            var grid = new ExpandableGrid<object>();
            var obj = new object();
            grid[0, MaxColumn] = obj;
            Assert.AreEqual(MaxColumn, grid.MaxColumn);
        }

        [TestMethod]
        public void TestMaxRpw()
        {
            const int MaxRow = 200;

            var grid = new ExpandableGrid<object>();
            var obj = new object();
            grid[MaxRow, 0] = obj;
            Assert.AreEqual(MaxRow, grid.MaxRow);
        }

        [TestMethod]
        public void TestMinRpw()
        {
            const int MinRow = 200;

            var grid = new ExpandableGrid<object>();
            var obj = new object();
            grid[MinRow, 0] = obj;
            Assert.AreEqual(MinRow, grid.MaxRow);
        }

        /// <summary>
        /// Test try get value with an empty set.
        /// </summary>
        [TestMethod]
        public void TestTryGetValueEmpty()
        {
            var grid = new ExpandableGrid<object>();
            var result = grid.TryGetValue(5, 5, out var value);
            Assert.IsFalse(result);
            Assert.IsNull(value);
        }

        /// <summary>
        /// Test get value empty.
        /// </summary>
        [TestMethod]
        public void TestGetValueEmpty()
        {
            var grid = new ExpandableGrid<object>();
            Assert.ThrowsException<KeyNotFoundException>(() => grid[0, 0]);
        }

        /// <summary>
        /// Test remove something which doesn't exist.
        /// </summary>
        [TestMethod]
        public void TestRemoveEmpty()
        {
            var grid = new ExpandableGrid<object>();
            Assert.IsFalse(grid.Remove(0, 0, out var value));
            Assert.IsNull(value);
        }

        /// <summary>
        /// Test remove something which doesn't exist.
        /// </summary>
        [TestMethod]
        public void TestRemove()
        {
            var grid = new ExpandableGrid<object>();

            var obj = new object();
            grid[4, 8] = obj;
            Assert.IsTrue(grid.Remove(4, 8, out var value));
            Assert.AreSame(obj, value);
        }
    }
}
