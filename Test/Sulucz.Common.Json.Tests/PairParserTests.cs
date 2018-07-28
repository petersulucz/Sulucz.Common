// <copyright file="PairParserTests.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common.Json.Tests
{
    using System.Dynamic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Sulucz.Common.Json.Internal;

    [TestClass]
    public class PairParserTests
    {
        /// <summary>
        /// Test a basic json pair.
        /// </summary>
        [TestMethod]
        public void TestBasicJsonPairString()
        {
            const string BasicPair = @"""test"" : ""string""
";
            var lex = UnitTestHelpers.GetLex(BasicPair);
            Assert.IsTrue(lex.TryGetNextToken(out var result));

            Assert.IsTrue(PairParser.TryParsePair(result.Value, lex, out var pair));

            Assert.AreEqual("test", pair.Key);
            Assert.IsInstanceOfType(pair.Value, typeof(string));
            Assert.AreEqual("string", pair.Value);
        }

        /// <summary>
        /// Test a basic json pair.
        /// </summary>
        [TestMethod]
        public void TestBasicJsonPairNumber()
        {
            const string BasicPair = @"""test"" : 100
";
            var lex = UnitTestHelpers.GetLex(BasicPair);
            Assert.IsTrue(lex.TryGetNextToken(out var result));

            Assert.IsTrue(PairParser.TryParsePair(result.Value, lex, out var pair));

            Assert.AreEqual("test", pair.Key);
            Assert.IsInstanceOfType(pair.Value, typeof(double));
            Assert.AreEqual(100, pair.Value);
        }

        /// <summary>
        /// Test a basic json pair.
        /// </summary>
        [TestMethod]
        public void TestBasicJsonPairBoolean()
        {
            const string BasicPair = @"""test"" : true
";
            var lex = UnitTestHelpers.GetLex(BasicPair);
            Assert.IsTrue(lex.TryGetNextToken(out var result));

            Assert.IsTrue(PairParser.TryParsePair(result.Value, lex, out var pair));

            Assert.AreEqual("test", pair.Key);
            Assert.IsInstanceOfType(pair.Value, typeof(bool));
            Assert.AreEqual(true, pair.Value);
        }

        /// <summary>
        /// Test a basic json pair.
        /// </summary>
        [TestMethod]
        public void TestBasicJsonPairArray()
        {
            const string BasicPair = @"""test"" : [ true, false]
";
            var lex = UnitTestHelpers.GetLex(BasicPair);
            Assert.IsTrue(lex.TryGetNextToken(out var result));

            Assert.IsTrue(PairParser.TryParsePair(result.Value, lex, out var pair));

            Assert.AreEqual("test", pair.Key);
            Assert.AreEqual(true, pair.Value[0]);
            Assert.AreEqual(false, pair.Value[1]);
        }

        /// <summary>
        /// Test a basic json pair.
        /// </summary>
        [TestMethod]
        public void TestBasicJsonPairObject()
        {
            const string BasicPair = @"""test"" : { ""key"" : ""true""
}
";
            var lex = UnitTestHelpers.GetLex(BasicPair);
            Assert.IsTrue(lex.TryGetNextToken(out var result));

            Assert.IsTrue(PairParser.TryParsePair(result.Value, lex, out var pair));

            Assert.AreEqual("test", pair.Key);
            Assert.IsInstanceOfType(pair.Value, typeof(ExpandoObject));
            Assert.AreEqual("true", pair.Value.key);
        }
    }
}
