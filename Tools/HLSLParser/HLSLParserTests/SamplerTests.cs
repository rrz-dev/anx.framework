using System;
using NUnit.Framework;
using HLSLParser;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace HLSLParserTests
{
	public static class SamplerTests
	{
		#region Constants
		private const string TestSampler =
			@"SamplerState MeshTextureSampler
{
    Filter = MIN_MAG_MIP_LINEAR;
    AddressU = Wrap;
    AddressV = Wrap;
};";

		private const string TestSamplerDx9 =
			@"sampler MeshTextureSampler = 
sampler_state
{
    Texture = <g_MeshTexture>;
    MipFilter = LINEAR;
    MinFilter = LINEAR;
    MagFilter = LINEAR;
};";
		#endregion

		#region ParseIfPass
		[Test]
		public static void ParseIfPass()
		{
			var text = new ParseTextWalker(TestSampler);
			var result = Sampler.TryParse(text);

			Assert.NotNull(result);
			Assert.AreEqual("MeshTextureSampler", result.Name);
			Assert.AreEqual("SamplerState", result.Type);

			Assert.AreEqual("Filter", result.States[0].Key);
			Assert.AreEqual("MIN_MAG_MIP_LINEAR", result.States[0].Value);
			Assert.AreEqual("AddressU", result.States[1].Key);
			Assert.AreEqual("Wrap", result.States[1].Value);
			Assert.AreEqual("AddressV", result.States[2].Key);
			Assert.AreEqual("Wrap", result.States[2].Value);
		}
		#endregion

		#region ParseIfPassDx9
		[Test]
		public static void ParseIfPassDx9()
		{
			var text = new ParseTextWalker(TestSamplerDx9);
			var result = Sampler.TryParse(text);

			Assert.NotNull(result);
			Assert.AreEqual("MeshTextureSampler", result.Name);
			Assert.AreEqual("sampler", result.Type);

			Assert.AreEqual("Texture", result.States[0].Key);
			Assert.AreEqual("<g_MeshTexture>", result.States[0].Value);
		}
		#endregion

		#region ParseIfPassHasRegister
		[Test]
		public static void ParseIfPassHasRegister()
		{
			var text = new ParseTextWalker("sampler TextureSampler : register(s0);");
			var result = Sampler.TryParse(text);

			Assert.NotNull(result);
			Assert.AreEqual("TextureSampler", result.Name);
			Assert.AreEqual("sampler", result.Type);
			Assert.AreEqual("register(s0)", result.Register);
		}
		#endregion

		#region TestParseIfTypeDefWithoutCode
		[Test]
		public static void TestParseIfTypeDefWithoutCode()
		{
			var text = new ParseTextWalker("testtest");
			var result = Sampler.TryParse(text);

			Assert.Null(result);
		}
		#endregion

		#region TestToString
		[Test]
		public static void TestToString()
		{
			var text = new ParseTextWalker(TestSampler);
			var result = Sampler.TryParse(text);

			string expected =
				@"SamplerState MeshTextureSampler
{
	Filter = MIN_MAG_MIP_LINEAR;
	AddressU = Wrap;
	AddressV = Wrap;
};";

			Assert.AreEqual(expected.Replace("\r", ""), result.ToString());
		}
		#endregion

		#region TestToStringWithRegister
		[Test]
		public static void TestToStringWithRegister()
		{
			var text = new ParseTextWalker("sampler TextureSampler : register(s0);");
			var result = Sampler.TryParse(text);

			string expected = "sampler TextureSampler : register(s0);";

			Assert.AreEqual(expected, result.ToString());
		}
		#endregion
	}
}
