using System;
using NUnit.Framework;
using HLSLParser;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace HLSLParserTests
{
	public static class VariableTests
	{
		#region TestGetTypeModifiersFromEmptyString
		[Test]
		public static void TestGetTypeModifiersFromEmptyString()
		{
			Assert.AreEqual(Variable.GetTypeModifiers(null), new string[0]);
			Assert.AreEqual(Variable.GetTypeModifiers(new ParseTextWalker("")),
				new string[0]);
		}
		#endregion

		#region TestGetTypeModifiersStringWithoutElement
		[Test]
		public static void TestGetTypeModifiersStringWithoutElement()
		{
			var text = new ParseTextWalker("aaa");
			string[] result = Variable.GetTypeModifiers(text);
			Assert.AreEqual(0, result.Length);
		}
		#endregion

		#region TestGetTypeModifiersFromStringWithoutSpace
		[Test]
		public static void TestGetTypeModifiersFromStringWithoutSpace()
		{
			var text = new ParseTextWalker("uniform");
			string[] result = Variable.GetTypeModifiers(text);
			Assert.AreEqual(1, result.Length);
			Assert.AreEqual("uniform", result[0]);
		}
		#endregion

		#region TestGetStorageClassesFrom
		[Test]
		public static void TestGetStorageClassesFrom()
		{
			var text = new ParseTextWalker("uniform extern float4x4 MatrixTransform;");
			string[] result = Variable.GetTypeModifiers(text);
			Assert.AreEqual(2, result.Length);
			Assert.AreEqual("uniform", result[0]);
			Assert.AreEqual("extern", result[1]);
		}
		#endregion

		#region TestIsVariableFollowing
		[Test]
		public static void TestIsVariableFollowing()
		{
			var text = new ParseTextWalker("int value;");
			bool result = Variable.IsVariableFollowing(text);
			Assert.True(result);

			text = new ParseTextWalker("int4x4 value;");
			result = Variable.IsVariableFollowing(text);
			Assert.True(result);

			text = new ParseTextWalker("const int value;");
			result = Variable.IsVariableFollowing(text);
			Assert.False(result);
		}
		#endregion

		#region TestParseIfVariable
		[Test]
		public static void TestParseIfVariable()
		{
			var text = new ParseTextWalker("int value;");
			var result = Variable.TryParse(text);
			Assert.NotNull(result);

			text = new ParseTextWalker("uniform const int value;");
			result = Variable.TryParse(text);
			Assert.NotNull(result);

			text = new ParseTextWalker("uniform extern float4x4 MatrixTransform;");
			result = Variable.TryParse(text);
			Assert.NotNull(result);

			text = new ParseTextWalker("PS_IN VS(VS_IN in) { }");
			result = Variable.TryParse(text);
			Assert.Null(result);
		}
		#endregion

		#region TestParseTwoVariables
		[Test]
		public static void TestParseTwoVariables()
		{
			string source =
				@"uniform extern float4x4 MatrixTransform;

Texture2D<float4> Texture : register(t0);";

			var text = new ParseTextWalker(source);
			var result = Variable.TryParse(text);

			Assert.NotNull(result);
			Assert.AreEqual("uniform extern float4x4 MatrixTransform;", result.ToString());
		}
		#endregion

		#region TestParseIfVariableWithGenericTexture2D
		[Test]
		public static void TestParseIfVariableWithGenericTexture2D()
		{
			string variableSource = "Texture2D<float4> Texture : register(t0);";
			var text = new ParseTextWalker(variableSource);
			var result = Variable.TryParse(text);
			Assert.NotNull(result);
			Assert.AreEqual("Texture2D<float4>", result.Type);
			Assert.AreEqual("Texture", result.Name);
			Assert.AreEqual("register(t0)", result.Register);
			Assert.AreEqual(variableSource, result.ToString());
		}
		#endregion
		
		#region TestfloatVariableParsing
		[Test]
		public static void TestfloatVariableParsing()
		{
			var text = new ParseTextWalker("uniform const float value;");
			var result = Variable.TryParse(text);
			Assert.NotNull(result);
			Assert.AreEqual(2, result.TypeModifiers.Length);
			Assert.AreEqual("float", result.Type);
		}
		#endregion

		#region TestVectorVariableParsing
		[Test]
		public static void TestVectorVariableParsing()
		{
			var text = new ParseTextWalker("vector<float, 1> value;");
			var result = Variable.TryParse(text);
			Assert.NotNull(result);
			Assert.AreEqual("float", result.Type);
			Assert.AreEqual(1, result.Dimensions.Length);
			Assert.AreEqual(1, result.Dimensions[0]);

			text = new ParseTextWalker("float4 value;");
			result = Variable.TryParse(text);
			Assert.NotNull(result);
			Assert.AreEqual("float", result.Type);
			Assert.AreEqual(1, result.Dimensions.Length);
			Assert.AreEqual(4, result.Dimensions[0]);
		}
		#endregion

		#region TestMatrixVariableParsing
		[Test]
		public static void TestMatrixVariableParsing()
		{
			var text = new ParseTextWalker("matrix<float, 1, 3> value;");
			var result = Variable.TryParse(text);
			Assert.NotNull(result);
			Assert.AreEqual("float", result.Type);
			Assert.AreEqual(2, result.Dimensions.Length);
			Assert.AreEqual(1, result.Dimensions[0]);
			Assert.AreEqual(3, result.Dimensions[1]);

			text = new ParseTextWalker("int4x4 value;");
			result = Variable.TryParse(text);
			Assert.NotNull(result);
			Assert.AreEqual("int", result.Type);
			Assert.AreEqual(2, result.Dimensions.Length);
			Assert.AreEqual(4, result.Dimensions[0]);
			Assert.AreEqual(4, result.Dimensions[1]);
		}
		#endregion

		#region TestNameParsing
		[Test]
		public static void TestNameParsing()
		{
			var text = new ParseTextWalker("matrix<float, 1, 3> value;");
			var result = Variable.TryParse(text);

			Assert.AreEqual("value", result.Name);
		}
		#endregion

		#region TestArrayParsing
		[Test]
		public static void TestArrayParsing()
		{
			var text = new ParseTextWalker("int value[15];");
			var result = Variable.TryParse(text);

			Assert.AreEqual("value", result.Name);
			Assert.AreEqual(15, result.ArraySize);
		}
		#endregion

		#region TestInitialValueParsing
		[Test]
		public static void TestInitialValueParsing()
		{
			var text = new ParseTextWalker("int value = 4;");
			var result = Variable.TryParse(text);

			Assert.AreEqual("value", result.Name);
			Assert.AreEqual("4", result.InitialValue);
		}
		#endregion

		#region TestInitialValueParsingWithoutValueText
		[Test]
		public static void TestInitialValueParsingWithoutValueText()
		{
			var text = new ParseTextWalker("int value;");
			var result = Variable.TryParse(text);

			Assert.AreEqual("value", result.Name);
			Assert.AreEqual(null, result.InitialValue);
		}
		#endregion

		#region TestAnnotationsParsing
		[Test]
		public static void TestAnnotationsParsing()
		{
			var text = new ParseTextWalker("int value<float y=1.3;>;");
			var result = Variable.TryParse(text);

			Assert.AreEqual("value", result.Name);
			Assert.AreEqual("float y=1.3;", result.Annotations);
		}
		#endregion

		#region TestAnnotationsParsingWithInitialValue
		[Test]
		public static void TestAnnotationsParsingWithInitialValue()
		{
			var text = new ParseTextWalker("int value<float y=1.3;> = 4;");
			var result = Variable.TryParse(text);

			Assert.AreEqual("value", result.Name);
			Assert.AreEqual("float y=1.3;", result.Annotations);
			Assert.AreEqual("4", result.InitialValue);
		}
		#endregion

		#region TestSemanticParsing
		[Test]
		public static void TestSemanticParsing()
		{
			var text = new ParseTextWalker("int value : COLOR;");
			var result = Variable.TryParse(text);
			
			Assert.AreEqual("value", result.Name);
			Assert.AreEqual("COLOR", result.Semantic);
		}
		#endregion

		#region TestSemanticParsingWithAnnotations
		[Test]
		public static void TestSemanticParsingWithAnnotations()
		{
			var text = new ParseTextWalker("int value : COLOR<float y=1.3;>;");
			var result = Variable.TryParse(text);

			Assert.AreEqual("value", result.Name);
			Assert.AreEqual("COLOR", result.Semantic);
			Assert.AreEqual("float y=1.3;", result.Annotations);
		}
		#endregion

		#region TestSemanticParsingWithInitialValue
		[Test]
		public static void TestSemanticParsingWithInitialValue()
		{
			var text = new ParseTextWalker("int value : COLOR = 5;");
			var result = Variable.TryParse(text);

			Assert.AreEqual("value", result.Name);
			Assert.AreEqual("COLOR", result.Semantic);
			Assert.AreEqual("5", result.InitialValue);
		}
		#endregion

		#region TestSemanticParsingWithoutSpaces
		[Test]
		public static void TestSemanticParsingWithoutSpaces()
		{
			var text = new ParseTextWalker("int value:COLOR;");
			var result = Variable.TryParse(text);

			Assert.AreEqual("value", result.Name);
			Assert.AreEqual("COLOR", result.Semantic);
		}
		#endregion

		#region TestPackoffsetParsing
		[Test]
		public static void TestPackoffsetParsing()
		{
			var text = new ParseTextWalker("int value : packoffset(c0);");
			var result = Variable.TryParse(text);

			Assert.AreEqual("value", result.Name);
			Assert.AreEqual("packoffset(c0)", result.Packoffset);
		}
		#endregion

		#region TestRegisterParsing
		[Test]
		public static void TestRegisterParsing()
		{
			var text = new ParseTextWalker("int value : register( ps_5_0, s );");
			var result = Variable.TryParse(text);

			Assert.AreEqual("value", result.Name);
			Assert.AreEqual("register( ps_5_0, s )", result.Register);
		}
		#endregion

		#region TestRegisterAndAnnotationParsing
		[Test]
		public static void TestRegisterAndAnnotationParsing()
		{
			var text = new ParseTextWalker(
				"int value : register( ps_5_0, s ) <float y=1.3;>;");
			var result = Variable.TryParse(text);

			Assert.AreEqual("value", result.Name);
			Assert.AreEqual("register( ps_5_0, s )", result.Register);
			Assert.AreEqual("float y=1.3;", result.Annotations);
		}
		#endregion

		#region TestToString
		[Test]
		public static void TestToString()
		{
			string variableText = "uniform const int value : COLOR <float y=1.3;> = 5;";
			var walker = new ParseTextWalker(variableText);
			var result = Variable.TryParse(walker);

			Assert.AreEqual(variableText, result.ToString());
		}
		#endregion

		#region TestToStringWithArraySize
		[Test]
		public static void TestToStringWithArraySize()
		{
			string variableText = "uniform const int value[7];";
			var walker = new ParseTextWalker(variableText);
			var result = Variable.TryParse(walker);

			Assert.AreEqual(variableText, result.ToString());
		}
		#endregion

		#region TestToStringWithPackoffset
		[Test]
		public static void TestToStringWithPackoffset()
		{
			string variableText = "uniform const int value : packoffset(c0) = 5;";
			var walker = new ParseTextWalker(variableText);
			var result = Variable.TryParse(walker);

			Assert.AreEqual(variableText, result.ToString());
		}
		#endregion

		#region TestToStringWithRegister
		[Test]
		public static void TestToStringWithRegister()
		{
			string variableText = "uniform const int value : register( vs, s[8] ) = 5;";
			var walker = new ParseTextWalker(variableText);
			var result = Variable.TryParse(walker);

			Assert.AreEqual(variableText, result.ToString());
		}
		#endregion

		#region TestToStringWithDimensions
		[Test]
		public static void TestToStringWithDimensions()
		{
			string variableText = "float4x4 matrix;";
			var walker = new ParseTextWalker(variableText);
			var result = Variable.TryParse(walker);

			Assert.AreEqual(variableText, result.ToString());
		}
		#endregion
	}
}
