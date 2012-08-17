using System;
using System.IO;
using HLSLParser;
using NUnit.Framework;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace HLSLParserTests
{
	public static class ParserTests
	{		
		#region TestLoadThrowsOnEmptyFilepath
		[Test]
		public static void TestLoadThrowsOnEmptyFilepath()
		{
			Assert.Throws(typeof(ArgumentException), delegate
			{
				Parser.LoadFromFile("");
			});
		}
		#endregion
		
		#region TestLoadThrowsOnMissingFile
		[Test]
		public static void TestLoadThrowsOnMissingFile()
		{
			Assert.Throws(typeof(FileNotFoundException), delegate
			{
				Parser.LoadFromFile("testfile.fx");
			});
		}
		#endregion
		
		#region TestWriteTestFile
		[Test]
		public static void TestWriteTestFile()
		{
			string testFilepath = HlslTestFile.WriteTestFile();
			Assert.True(File.Exists(testFilepath));
			File.Delete(testFilepath);
		}
		#endregion

		#region TestLoadFile
		[Test]
		public static void TestLoadFile()
		{
			string testFilepath = HlslTestFile.WriteTestFile();
			Parser parser = Parser.LoadFromFile(testFilepath);

			Assert.NotNull(parser.Effect);
			Assert.NotNull(parser.Effect.Source);
			Assert.AreNotEqual(parser.Effect.Source, "");

			File.Delete(testFilepath);
		}
		#endregion

		#region TestParseFile
		[Test]
		public static void TestParseFile()
		{
			string testFilepath = HlslTestFile.WriteTestFile();
			Parser parser = Parser.LoadFromFile(testFilepath);

			parser.Parse();

			Assert.AreEqual(2, parser.Effect.Variables.Count);
			Assert.AreEqual(1, parser.Effect.Techniques.Count);
			Assert.AreEqual(2, parser.Effect.Methods.Count);
			Assert.AreEqual(2, parser.Effect.Structures.Count);
			Assert.AreEqual(1, parser.Effect.Samplers.Count);
			Assert.AreEqual(0, parser.Effect.TypeDefs.Count);
			Assert.AreEqual(0, parser.Effect.Buffers.Count);

			File.Delete(testFilepath);
		}
		#endregion

		#region TestParseFromSource
		[Test]
		public static void TestParseFromSource()
		{
			Parser parser = Parser.LoadFromSource(HlslTestFile.Source);

			parser.Parse();

			Assert.AreEqual(2, parser.Effect.Variables.Count);
			Assert.AreEqual(1, parser.Effect.Techniques.Count);
			Assert.AreEqual(2, parser.Effect.Methods.Count);
			Assert.AreEqual(2, parser.Effect.Structures.Count);
			Assert.AreEqual(1, parser.Effect.Samplers.Count);
			Assert.AreEqual(0, parser.Effect.TypeDefs.Count);
			Assert.AreEqual(0, parser.Effect.Buffers.Count);
		}
		#endregion

		#region TestParseWithTypeDef
		[Test]
		public static void TestParseWithTypeDef()
		{
			Parser parser = Parser.LoadFromSource("typedef matrix <bool, 1, 1> bool1x1;");

			parser.Parse();

			Assert.AreEqual(1, parser.Effect.TypeDefs.Count);
		}
		#endregion

		#region TestParseWithBuffer
		[Test]
		public static void TestParseWithBuffer()
		{
			Parser parser = Parser.LoadFromSource("Buffer<float4> g_Buffer;");

			parser.Parse();

			Assert.AreEqual(1, parser.Effect.Buffers.Count);
		}
		#endregion
	}
}
