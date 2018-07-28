// <copyright file="LexicalAnalyzerTests.cs" company="Peter Sulucz">
// Copyright (c) Peter Sulucz. All rights reserved.
// </copyright>

namespace Sulucz.Common.Json.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Sulucz.Common.Json.Internal;

    [TestClass]
    public class LexicalAnalyzerTests
    {
        /// <summary>
        /// Test lex on an empty string.
        /// </summary>
        [TestMethod]
        public void TestEmptyString()
        {
            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(string.Empty))
            {
                var lex = new LexicalAnalyzer(reader);
                Assert.IsFalse(lex.TryGetNextToken(out _));
                Assert.IsTrue(reader.EndOfStream);
            }
        }

        /// <summary>
        /// Test lex on a whitespace string.
        /// </summary>
        [TestMethod]
        public void TestWhitespaceString()
        {
            const string TestString = @"

    
                    
";

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                var lex = new LexicalAnalyzer(reader);
                Assert.IsFalse(lex.TryGetNextToken(out _));
                Assert.IsTrue(reader.EndOfStream);
            }
        }

        /// <summary>
        /// Test a basic object openner.
        /// </summary>
        [TestMethod]
        public void TestBasicObject()
        {
            const string TestString = @"{}";

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                var lex = new LexicalAnalyzer(reader);
                Assert.IsTrue(lex.TryGetNextToken(out var begin));
                Assert.AreEqual("{", begin.Value);

                Assert.IsTrue(lex.TryGetNextToken(out var end));
                Assert.AreEqual("}", end.Value);

                Assert.IsTrue(reader.EndOfStream);
            }
        }

        /// <summary>
        /// Test a basic object openner.
        /// </summary>
        [TestMethod]
        public void TestBasicObject2()
        {
            const string TestString = @"
    {
    
                    }
";

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                var lex = new LexicalAnalyzer(reader);
                Assert.IsTrue(lex.TryGetNextToken(out var begin));
                Assert.AreEqual("{", begin.Value);

                Assert.IsTrue(lex.TryGetNextToken(out var end));
                Assert.AreEqual("}", end.Value);

                Assert.IsFalse(lex.TryGetNextToken(out _));

                Assert.IsTrue(reader.EndOfStream);
            }
        }

        /// <summary>
        /// Test a basic array.
        /// </summary>
        [TestMethod]
        public void TestBasicArray()
        {
            const string TestString = @"[]";

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                var lex = new LexicalAnalyzer(reader);
                Assert.IsTrue(lex.TryGetNextToken(out var begin));
                Assert.AreEqual("[", begin.Value);

                Assert.IsTrue(lex.TryGetNextToken(out var end));
                Assert.AreEqual("]", end.Value);

                Assert.IsTrue(reader.EndOfStream);
            }
        }

        /// <summary>
        /// Test a basic array.
        /// </summary>
        [TestMethod]
        public void TestComplexArray()
        {
            const string TestString = @"[ { ""test"" : true },
{ ""test2"" : false}]";

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                var lex = new LexicalAnalyzer(reader);
                Assert.IsTrue(lex.TryGetNextToken(out var current));
                Assert.AreEqual("[", current.Value);

                Assert.IsTrue(lex.TryGetNextToken(out current));
                Assert.AreEqual("{", current.Value);

                Assert.IsTrue(lex.TryGetNextToken(out current));
                Assert.AreEqual("test", current.Value);

                Assert.IsTrue(lex.TryGetNextToken(out current));
                Assert.AreEqual(":", current.Value);

                Assert.IsTrue(lex.TryGetNextToken(out current));
                Assert.AreEqual("true", current.Value);

                Assert.IsTrue(lex.TryGetNextToken(out current));
                Assert.AreEqual("}", current.Value);

                Assert.IsTrue(lex.TryGetNextToken(out current));
                Assert.AreEqual(",", current.Value);

                Assert.IsTrue(lex.TryGetNextToken(out current));
                Assert.AreEqual("{", current.Value);

                Assert.IsTrue(lex.TryGetNextToken(out current));
                Assert.AreEqual("test2", current.Value);

                Assert.IsTrue(lex.TryGetNextToken(out current));
                Assert.AreEqual(":", current.Value);

                Assert.IsTrue(lex.TryGetNextToken(out current));
                Assert.AreEqual("false", current.Value);

                Assert.IsTrue(lex.TryGetNextToken(out current));
                Assert.AreEqual("}", current.Value);

                Assert.IsTrue(lex.TryGetNextToken(out current));
                Assert.AreEqual("]", current.Value);

                Assert.IsTrue(reader.EndOfStream);
            }
        }

        /// <summary>
        /// Test a basic string
        /// </summary>
        [TestMethod]
        public void TestBasicString()
        {
            const string TestString = "\"test\"";

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                var lex = new LexicalAnalyzer(reader);
                Assert.IsTrue(lex.TryGetNextToken(out var token));
                Assert.AreEqual("test", token.Value);

                Assert.IsFalse(lex.TryGetNextToken(out _));

                Assert.IsTrue(reader.EndOfStream);
            }
        }

        /// <summary>
        /// Test a basic string
        /// </summary>
        [TestMethod]
        public void TestBasicString2()
        {
            const string TestString = @"
        ""test""
";

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                var lex = new LexicalAnalyzer(reader);
                Assert.IsTrue(lex.TryGetNextToken(out var token));
                Assert.AreEqual("test", token.Value);

                Assert.IsFalse(lex.TryGetNextToken(out _));

                Assert.IsTrue(reader.EndOfStream);
            }
        }

        /// <summary>
        /// Test reading just a basic string token function.
        /// </summary>
        [TestMethod]
        public void ReadStringTokenTest()
        {
            const string TestString = "";

            var builder = new StringBuilder();

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                builder.Clear();

                var result = LexicalAnalyzer.ReadStringToken(reader, builder);
                Assert.IsFalse(result);
                Assert.IsTrue(reader.EndOfStream);
                Assert.AreEqual(0, builder.Length);
            }
        }

        /// <summary>
        /// Test reading just a basic string token function.
        /// </summary>
        [TestMethod]
        public void ReadStringTokenTestWhite()
        {
            const string TestString = "      ";

            var builder = new StringBuilder();

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                builder.Clear();

                var result = LexicalAnalyzer.ReadStringToken(reader, builder);
                Assert.IsFalse(result);
                Assert.IsTrue(reader.EndOfStream);
            }
        }

        /// <summary>
        /// Test reading just a basic string token function.
        /// </summary>
        [TestMethod]
        public void ReadStringTokenTestNolength()
        {
            const string TestString = "\"   ";

            var builder = new StringBuilder();

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                builder.Clear();

                var result = LexicalAnalyzer.ReadStringToken(reader, builder);
                Assert.IsTrue(result);
                Assert.AreEqual(0, builder.Length);
            }
        }

        /// <summary>
        /// Test reading just a basic string token function.
        /// </summary>
        [TestMethod]
        public void ReadStringTokenTestValid()
        {
            const string TestString = "yolo\" pant  ";

            var builder = new StringBuilder();

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                builder.Clear();

                var result = LexicalAnalyzer.ReadStringToken(reader, builder);
                Assert.IsTrue(result);
                Assert.AreEqual("yolo", builder.ToString());
            }
        }

        /// <summary>
        /// Test reading just a basic string token function with valid whitespace.
        /// </summary>
        [TestMethod]
        public void ReadStringTokenTestValidWhitespace()
        {
            const string TestString = "   yolo\" pant  ";

            var builder = new StringBuilder();

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                builder.Clear();

                var result = LexicalAnalyzer.ReadStringToken(reader, builder);
                Assert.IsTrue(result);
                Assert.AreEqual("   yolo", builder.ToString());
            }
        }

        /// <summary>
        /// Test reading just a basic string token function with valid whitespace.
        /// </summary>
        [TestMethod]
        public void ReadStringTokenWithEscape()
        {
            const string TestString = "\\\"yolo\" pant  ";

            var builder = new StringBuilder();

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                builder.Clear();

                var result = LexicalAnalyzer.ReadStringToken(reader, builder);
                Assert.IsTrue(result);
                Assert.AreEqual("\\\"yolo", builder.ToString());
            }
        }

        /// <summary>
        /// Basic test of readliteral.
        /// </summary>
        [TestMethod]
        public void ReadLiteralTest()
        {
            const string TestString = "true";

            var builder = new StringBuilder();

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                builder.Clear();

                var current = (char)reader.Read();

                var result = LexicalAnalyzer.ReadLiteral(current, reader, builder);
                Assert.IsTrue(result);
                Assert.AreEqual("true", builder.ToString());
            }
        }

        /// <summary>
        /// Basic test of readliteral.
        /// </summary>
        [TestMethod]
        public void ReadLiteralTest2()
        {
            const string TestString = "false    ";

            var builder = new StringBuilder();

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                builder.Clear();

                var current = (char)reader.Read();

                var result = LexicalAnalyzer.ReadLiteral(current, reader, builder);
                Assert.IsTrue(result);
                Assert.AreEqual("false", builder.ToString());
            }
        }

        /// <summary>
        /// Basic test of readliteral.
        /// </summary>
        [TestMethod]
        public void ReadLiteralTestNumber()
        {
            const string TestString = "1.123455";

            var builder = new StringBuilder();

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                builder.Clear();

                var current = (char)reader.Read();

                var result = LexicalAnalyzer.ReadLiteral(current, reader, builder);
                Assert.IsTrue(result);
                Assert.AreEqual("1.123455", builder.ToString());
            }
        }

        /// <summary>
        /// Basic test of read literal with an array bracket.
        /// </summary>
        [TestMethod]
        public void ReadLiteralNumberWithArray()
        {
            const string TestString = "1.0]";

            var builder = new StringBuilder();

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                builder.Clear();

                var current = (char)reader.Read();

                var result = LexicalAnalyzer.ReadLiteral(current, reader, builder);
                Assert.IsTrue(result);
                Assert.AreEqual("1.0", builder.ToString());
            }
        }

        /// <summary>
        /// Basic test of parsing the value true.
        /// </summary>
        [TestMethod]
        public void TestParseTrue()
        {
            const string TestString = "true";

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                var lex = new LexicalAnalyzer(reader);

                Assert.IsTrue(lex.TryGetNextToken(out var token));
                Assert.AreEqual("true", token.Value);
            }
        }

        /// <summary>
        /// Basic test of parsing the value true.
        /// </summary>
        [TestMethod]
        public void TestParseTrueMessy()
        {
            const string TestString = " { true   }  ";

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                var lex = new LexicalAnalyzer(reader);

                Assert.IsTrue(lex.TryGetNextToken(out var token));
                Assert.AreEqual("{", token.Value);

                Assert.IsTrue(lex.TryGetNextToken(out token));
                Assert.AreEqual("true", token.Value);

                Assert.IsTrue(lex.TryGetNextToken(out token));
                Assert.AreEqual("}", token.Value);

                Assert.IsFalse(lex.TryGetNextToken(out _));
            }
        }

        /// <summary>
        /// Basic test of parsing the value true.
        /// </summary>
        [TestMethod]
        public void TestParseNumberMessy()
        {
            const string TestString = " { 1.3243e10   }  ";

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                var lex = new LexicalAnalyzer(reader);

                Assert.IsTrue(lex.TryGetNextToken(out var token));
                Assert.AreEqual("{", token.Value);

                Assert.IsTrue(lex.TryGetNextToken(out token));
                Assert.AreEqual("1.3243e10", token.Value);

                Assert.IsTrue(lex.TryGetNextToken(out token));
                Assert.AreEqual("}", token.Value);

                Assert.IsFalse(lex.TryGetNextToken(out _));
            }
        }

        /// <summary>
        /// Basic test of parsing the value true.
        /// </summary>
        [TestMethod]
        public void TestParseNegativeNumberMessy()
        {
            const string TestString = " {  -1.444e234   }  ";

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                var lex = new LexicalAnalyzer(reader);

                Assert.IsTrue(lex.TryGetNextToken(out var token));
                Assert.AreEqual("{", token.Value);

                Assert.IsTrue(lex.TryGetNextToken(out token));
                Assert.AreEqual("-1.444e234", token.Value);

                Assert.IsTrue(lex.TryGetNextToken(out token));
                Assert.AreEqual("}", token.Value);

                Assert.IsFalse(lex.TryGetNextToken(out _));
            }
        }

        /// <summary>
        /// Tests the colon.
        /// </summary>
        [TestMethod]
        public void TestColon()
        {
            const string TestString = " :  ";

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                var lex = new LexicalAnalyzer(reader);

                Assert.IsTrue(lex.TryGetNextToken(out var token));
                Assert.AreEqual(":", token.Value);

                Assert.IsFalse(lex.TryGetNextToken(out _));
            }
        }

        /// <summary>
        /// Tests the comma.
        /// </summary>
        [TestMethod]
        public void TestComma()
        {
            const string TestString = " ,  ";

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                var lex = new LexicalAnalyzer(reader);

                Assert.IsTrue(lex.TryGetNextToken(out var token));
                Assert.AreEqual(",", token.Value);

                Assert.IsFalse(lex.TryGetNextToken(out _));
            }
        }

        /// <summary>
        /// Test a basic pair.
        /// </summary>
        [TestMethod]
        public void TestbasicObject()
        {
            const string TestString = " { \"hello\" : true } ";

            foreach (var reader in LexicalAnalyzerTests.GetStreamReader(TestString))
            {
                var lex = new LexicalAnalyzer(reader);

                Assert.IsTrue(lex.TryGetNextToken(out var token));
                Assert.AreEqual("{", token.Value);

                Assert.IsTrue(lex.TryGetNextToken(out token));
                Assert.AreEqual("hello", token.Value);

                Assert.IsTrue(lex.TryGetNextToken(out token));
                Assert.AreEqual(":", token.Value);

                Assert.IsTrue(lex.TryGetNextToken(out token));
                Assert.AreEqual("true", token.Value);

                Assert.IsTrue(lex.TryGetNextToken(out token));
                Assert.AreEqual("}", token.Value);

                Assert.IsFalse(lex.TryGetNextToken(out _));
            }
        }

        /// <summary>
        /// Gets stream readers for tested encodings.
        /// </summary>
        /// <param name="testString">The test string.</param>
        /// <returns>The reader.</returns>
        private static IEnumerable<StreamReader> GetStreamReader(string testString)
        {
            var encodings = new[] { Encoding.ASCII, Encoding.Unicode };

            foreach (var encoding in encodings)
            {
                var bytes = encoding.GetBytes(testString);
                var stream = new MemoryStream(bytes);
                var reader = new StreamReader(stream, encoding);

                yield return reader;
            }
        }
    }
}
