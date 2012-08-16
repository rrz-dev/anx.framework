using System;
using NUnit.Framework;
using HLSLParser;

namespace HLSLParserTests
{
	public static class CommentRemoverTests
	{
		#region TestEmptySource
		[Test]
		public static void TestEmptySource()
		{
			Assert.AreEqual(CommentRemover.Remove(null), null);
			Assert.AreEqual(CommentRemover.Remove(""), "");
		}
		#endregion

		#region TestMultilineComment
		[Test]
		public static void TestMultilineComment()
		{
			string source = "/* comment line */";
			Assert.AreEqual(CommentRemover.Remove(source), "");
			source = "/* comment line\nline two*/";
			Assert.AreEqual(CommentRemover.Remove(source), "");
		}
		#endregion

		#region TestCascadedMultilineComments
		[Test]
		public static void TestCascadedMultilineComments()
		{
			string source = "/* /*comment line */";
			Assert.AreEqual(CommentRemover.Remove(source), "");
			source = "/* comment /* //line\nline two*/";
			Assert.AreEqual(CommentRemover.Remove(source), "");
		}
		#endregion

		#region TestMultilineCommentWithCodeInFront
		[Test]
		public static void TestMultilineCommentWithCodeInFront()
		{
			string source = "aaa/*comment*/";
			Assert.AreEqual(CommentRemover.Remove(source), "aaa");
		}
		#endregion

		#region TestMultipleMultilineComments
		[Test]
		public static void TestMultipleMultilineComments()
		{
			string source = "/*blub*/aaa/*comment*/";
			Assert.AreEqual(CommentRemover.Remove(source), "aaa");
		}
		#endregion

		#region TestMultilineCommentWithCodeAfter
		[Test]
		public static void TestMultilineCommentWithCodeAfter()
		{
			string source = "/*comment*/bbb";
			Assert.AreEqual(CommentRemover.Remove(source), "bbb");
		}
		#endregion

		#region TestSingleLineComment
		[Test]
		public static void TestSingleLineComment()
		{
			string source = "// comment line";
			Assert.AreEqual(CommentRemover.Remove(source), "");
			source = "// comment line\n//test";
			Assert.AreEqual(CommentRemover.Remove(source), "");
		}
		#endregion

		#region TestSingleLineCommentWithCode
		[Test]
		public static void TestSingleLineCommentWithCode()
		{
			string source =
				@"// comment line
float4x4 matrix;";
			Assert.AreEqual(CommentRemover.Remove(source), "float4x4 matrix;");
		}
		#endregion

		#region TestSingleLineCommentWithCodeInFront
		[Test]
		public static void TestSingleLineCommentWithCodeInFront()
		{
			string source = "float value;// comment line";
			Assert.AreEqual(CommentRemover.Remove(source), "float value;");
		}
		#endregion

		#region TestSplitLines
		[Test]
		public static void TestSplitLines()
		{
			string source = "aa\nbbb";
			Assert.AreEqual(CommentRemover.SplitLines(source), new string[] { "aa", "bbb" });
			source = @"aa
bbb";
			Assert.AreEqual(CommentRemover.SplitLines(source), new string[] { "aa", "bbb" });
			source = "aa\n\rbbb";
			Assert.AreEqual(CommentRemover.SplitLines(source), new string[] { "aa", "bbb" });
		}
		#endregion

		#region TestMergeLines
		[Test]
		public static void TestMergeLines()
		{
			string[] lines = new string[] { "aa", "bbb" };
			Assert.AreEqual(CommentRemover.MergeLines(lines), "aa\nbbb");
			lines = new string[] { "aa", "", null, "bbb" };
			Assert.AreEqual(CommentRemover.MergeLines(lines), "aa\nbbb");
		}
		#endregion
	}
}
