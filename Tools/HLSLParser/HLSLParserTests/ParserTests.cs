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
			Assert.AreEqual(parser.Effect.Result, null);

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

			Assert.AreNotEqual("", parser.Effect.Result);
			Assert.Greater(parser.Effect.Result.Length, 0);

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
	}
}
