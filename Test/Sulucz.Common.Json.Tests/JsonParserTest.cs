// <copyright file="JsonParserTest.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common.Json.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class JsonParserTest
    {
        /// <summary>
        /// Test the json parser on a basic object.
        /// </summary>
        [TestMethod]
        public void TestBasicObject()
        {
            const string TestString = "{}";
            foreach (var stream in UnitTestHelpers.CreateStringStream(TestString))
            {
                var parser = new JsonParser(stream.Item1, stream.Item2);

                Assert.IsTrue(parser.TryParse(out var result));
            }
        }

        /// <summary>
        /// Test the json parser on a basic object.
        /// </summary>
        [TestMethod]
        public void TestBasicArray()
        {
            const string TestString = "[]";
            foreach (var stream in UnitTestHelpers.CreateStringStream(TestString))
            {
                var parser = new JsonParser(stream.Item1, stream.Item2);

                Assert.IsTrue(parser.TryParse(out var result));
            }
        }

        /// <summary>
        /// Test the json parser on a basic object.
        /// </summary>
        [TestMethod]
        public void TestObjectWithOneProperty()
        {
            const string TestString = "{\"one\":\"propdoe\"}";
            foreach (var stream in UnitTestHelpers.CreateStringStream(TestString))
            {
                var parser = new JsonParser(stream.Item1, stream.Item2);

                Assert.IsTrue(parser.TryParse(out var result));
                Assert.AreEqual("propdoe", result.one);
            }
        }

        /// <summary>
        /// Test the json parser on an array with one item.
        /// </summary>
        [TestMethod]
        public void TestArrayWithOneItem()
        {
            const string TestString = "[\"yolo\"]";
            foreach (var stream in UnitTestHelpers.CreateStringStream(TestString))
            {
                var parser = new JsonParser(stream.Item1, stream.Item2);

                Assert.IsTrue(parser.TryParse(out var result));
                Assert.AreEqual("yolo", result[0]);
            }
        }
    }
}
