// <copyright file="ObjectParserTests.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common.Json.Tests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Sulucz.Common.Json.Internal;

    [TestClass]
    public class ObjectParserTests
    {
        /// <summary>
        /// Test basic empty object.
        /// </summary>
        [TestMethod]
        public void TestBasicObject()
        {
            var lex = UnitTestHelpers.GetLex("{ }");

            Assert.IsTrue(lex.TryGetNextToken(out var firstToken));
            Assert.IsTrue(ObjectParser.TryParseObject(firstToken, lex, out var result));

            var dict = result as IDictionary<string, object>;
            Assert.IsNotNull(dict);
            Assert.AreEqual(0, dict.Count);
        }

        /// <summary>
        /// Test basic object.
        /// </summary>
        [TestMethod]
        public void TestBasicObjectSingleStringPair()
        {
            var lex = UnitTestHelpers.GetLex("{ \"hashtag\":\"yolo\"  }");

            Assert.IsTrue(lex.TryGetNextToken(out var firstToken));
            Assert.IsTrue(ObjectParser.TryParseObject(firstToken, lex, out var result));

            var dict = result as IDictionary<string, object>;
            Assert.IsNotNull(dict);
            Assert.AreEqual(1, dict.Count);
            Assert.AreEqual("yolo", dict["hashtag"]);
        }

        /// <summary>
        /// Test basic object with a number.
        /// </summary>
        [TestMethod]
        public void TestBasicObjectSingleNumberPair()
        {
            var lex = UnitTestHelpers.GetLex("{ \"hashtag\": 1000  }");

            Assert.IsTrue(lex.TryGetNextToken(out var firstToken));
            Assert.IsTrue(ObjectParser.TryParseObject(firstToken, lex, out var result));

            var dict = result as IDictionary<string, object>;
            Assert.IsNotNull(dict);
            Assert.AreEqual(1, dict.Count);
            Assert.AreEqual(1000.0, dict["hashtag"]);
        }

        /// <summary>
        /// Test basic object with a number.
        /// </summary>
        [TestMethod]
        public void TestBasicObjectWithTwoPairs()
        {
            var lex = UnitTestHelpers.GetLex("{ \"one\": 1000, \"two\" : \"yee\", \"three\": true,  }");

            Assert.IsTrue(lex.TryGetNextToken(out var firstToken));
            Assert.IsTrue(ObjectParser.TryParseObject(firstToken, lex, out var result));

            var dict = result as IDictionary<string, object>;
            Assert.IsNotNull(dict);
            Assert.AreEqual(3, dict.Count);
            Assert.AreEqual(1000.0, dict["one"]);
            Assert.AreEqual("yee", dict["two"]);
        }
    }
}
