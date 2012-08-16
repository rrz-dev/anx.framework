using System;
using NUnit.Framework;
using HLSLParser;

namespace HLSLParserTests
{
	public static class BufferTests
	{
		#region TestParseIfBuffer
		[Test]
		public static void TestParseIfBuffer()
		{
			var text = new ParseTextWalker("Buffer<float4> g_Buffer;");
			var result = EffectBuffer.ParseIfBuffer(text);

			Assert.NotNull(result);
			Assert.AreEqual("g_Buffer", result.Name);
			Assert.AreEqual("float4", result.Type);
		}
		#endregion

		#region TestParseIfBufferWithoutCode
		[Test]
		public static void TestParseIfBufferWithoutCode()
		{
			var text = new ParseTextWalker("testtest");
			var result = EffectBuffer.ParseIfBuffer(text);

			Assert.Null(result);
		}
		#endregion

		#region TestToString
		[Test]
		public static void TestToString()
		{
			string text = "Buffer<float4> g_Buffer;";
			var walker = new ParseTextWalker(text);
			var result = EffectBuffer.ParseIfBuffer(walker);

			Assert.AreEqual(text, result.ToString());
		}
		#endregion
	}
}
