// <copyright file="ArrayParserTests.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common.Json.Tests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Sulucz.Common.Json.Internal;

    /// <summary>
    /// The Array parser test.
    /// </summary>
    [TestClass]
    public class ArrayParserTests
    {
        /// <summary>
        /// Test an empty array.
        /// </summary>
        [TestMethod]
        public void TestEmptyArray()
        {
            var lex = UnitTestHelpers.GetLex("[]");
            Assert.IsTrue(lex.TryGetNextToken(out var initial));
            Assert.AreEqual(TokenType.ArrayOpen, initial.Type);
            Assert.IsTrue(ArrayParser.TryParseArray(initial, lex, out var array));
            Assert.AreEqual(0, array.Count);
        }

        /// <summary>
        /// Test an array with one element.
        /// </summary>
        [TestMethod]
        public void TestArrayWithOneElement()
        {
            var lex = UnitTestHelpers.GetLex("[1]");
            Assert.IsTrue(lex.TryGetNextToken(out var initial));
            Assert.IsTrue(ArrayParser.TryParseArray(initial, lex, out var array));
            Assert.AreEqual(1, array.Count);
        }

        /// <summary>
        /// Test a boolean array.
        /// </summary>
        [TestMethod]
        public void TestBoolArray()
        {
            var lex = UnitTestHelpers.GetLex("[true,true,true,false,FALSE,fAlSe]");
            Assert.IsTrue(lex.TryGetNextToken(out var initial));
            Assert.IsTrue(ArrayParser.TryParseArray(initial, lex, out var array));
            Assert.AreEqual(6, array.Count);
            Assert.IsTrue(array.Take(3).All(v => v));
            Assert.IsTrue(array.Skip(3).All(v => !v));
        }

        /// <summary>
        /// Test a number array.
        /// </summary>
        [TestMethod]
        public void TestNumberArray()
        {
            var lex = UnitTestHelpers.GetLex("[ 1, 2, 3, 4, 5, 6 ]");
            Assert.IsTrue(lex.TryGetNextToken(out var initial));
            Assert.IsTrue(ArrayParser.TryParseArray(initial, lex, out var array));
            Assert.AreEqual(6, array.Count);
        }

        /// <summary>
        /// Test a none closed array.
        /// </summary>
        [TestMethod]
        public void TestNonClosedArray()
        {
            var lex = UnitTestHelpers.GetLex("[ 1, 2, 3, 4, 5, 6 ");
            Assert.IsTrue(lex.TryGetNextToken(out var initial));
            Assert.IsFalse(ArrayParser.TryParseArray(initial, lex, out var array));
        }

        /// <summary>
        /// Test an array of arrays.
        /// </summary>
        [TestMethod]
        public void TestArrayOfArrays()
        {
            var lex = UnitTestHelpers.GetLex("[ [1], [2],[3], [4],[5]] ");
            Assert.IsTrue(lex.TryGetNextToken(out var initial));
            Assert.IsTrue(ArrayParser.TryParseArray(initial, lex, out var array));

            for (var i = 0; i < 5; i++)
            {
                Assert.AreEqual(i + 1, array[i][0]);
            }
        }
    }
}
