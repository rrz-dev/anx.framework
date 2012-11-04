using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Helpers.HLSLParser
{
	public class Pass : IShaderElement
	{
		#region Private
		private string vertexShaderProfile = "";
		private string pixelShaderProfile = "";
		#endregion

		#region Public
		public string Name
		{
			get;
			private set;
		}

		public string VertexShader
		{
			get;
			private set;
		}

		public string VertexShaderProfile
		{
			get
			{
				return vertexShaderProfile;
			}
		}

		public string PixelShader
		{
			get;
			private set;
		}

		public string PixelShaderProfile
		{
			get
			{
				return pixelShaderProfile;
			}
		}
		#endregion

		#region Constructor
		public Pass(ParseTextWalker walker)
		{
			string text = walker.Text;

			int indexOfNameStart = text.IndexOf(' ') + 1;
			int indexOfNameEnd = text.IndexOf('{');

			Name = text.Substring(indexOfNameStart, indexOfNameEnd - indexOfNameStart);
			Name = Name.TrimEnd('\n', '\r', '\t', ' ');
			
			string passContentText = text.Substring(indexOfNameEnd + 1);
			if (text.Contains("SetVertexShader"))
			{
				passContentText = passContentText.Replace("\t", "").Replace(" ", "");
				passContentText = passContentText.Replace(",", ", ");
				ParseDx10Pass(passContentText);
			}
			else
			{
				ParseDx9Pass(passContentText);
			}

			walker.Seek(text.IndexOf('}') + 1);
		}
		#endregion

		#region ParseDx10Pass
		private void ParseDx10Pass(string text)
		{
			VertexShader = ParseDx10Shader(text, "SetVertexShader(", ref vertexShaderProfile);
			PixelShader = ParseDx10Shader(text, "SetPixelShader(", ref pixelShaderProfile);
		}
		#endregion

		#region ParseDx10Shader
		private string ParseDx10Shader(string text, string key, ref string profile)
		{
			int indexOfshaderStart = text.IndexOf(key) + key.Length;
			int indexOfPixelShaderEnd = text.IndexOf(");", indexOfshaderStart);
			string shader = text.Substring(indexOfshaderStart,
				indexOfPixelShaderEnd - indexOfshaderStart);
			shader = shader.Replace(" ", "").Replace("\t", "");
			shader = shader.Replace("\r", "").Replace("\n", "");

			int subPartStartIndex = "CompileShader(".Length;
			shader = shader.Substring(subPartStartIndex,
				shader.Length - subPartStartIndex - 1);

			int indexOfFirstComma = shader.IndexOf(',');
			profile = shader.Substring(0, indexOfFirstComma);
			shader = shader.Substring(indexOfFirstComma + 1);
			shader = shader.Replace(",", ", ");

			return shader;
		}
		#endregion
		
		#region ParseDx9Pass
		private void ParseDx9Pass(string text)
		{
			VertexShader = ParseDx9Shader(text, "VertexShader", ref vertexShaderProfile);
			PixelShader = ParseDx9Shader(text, "PixelShader", ref pixelShaderProfile);
		}
		#endregion

		#region ParseDx9Shader
		private string ParseDx9Shader(string text, string key, ref string profile)
		{
			int indexOfShaderStart = text.IndexOf(key);
			string shader = "";
			indexOfShaderStart = text.IndexOf("compile ", indexOfShaderStart);
			int indexOfShaderEnd = text.IndexOf(';', indexOfShaderStart);
			shader = text.Substring(indexOfShaderStart,
				indexOfShaderEnd - indexOfShaderStart);
			shader = shader.Replace("compile", "");
			shader = shader.Trim();

			int indexOfSpaceAfterProfile = shader.IndexOf(' ');
			profile = shader.Substring(0, indexOfSpaceAfterProfile);

			shader = shader.Substring(indexOfSpaceAfterProfile).Trim();
			shader = shader.Replace(" ", "").Replace(",", ", ");
			return shader;
		}
		#endregion

		#region TryParse
		public static Pass TryParse(ParseTextWalker walker)
		{
			return walker.Text.StartsWith("pass ") ?
				new Pass(walker) :
				null;
		}
		#endregion

		#region ToString
		public override string ToString()
		{
			return ToStringIndented(0);
		}
		#endregion

		#region ToStringIndented
		public string ToStringIndented(int tabs)
		{
			string idention = "";
			for (int tabIndex = 0; tabIndex < tabs; tabIndex++)
			{
				idention += "\t";
			}

			string text = idention + "pass " + Name + "\n" + idention + "{\n";

			text += idention + "\tSetVertexShader(CompileShader(" +
				VertexShaderProfile + ", " + VertexShader + "));\n";
			text += idention + "\tSetGeometryShader(NULL);\n";
			text += idention + "\tSetPixelShader(CompileShader(" +
				PixelShaderProfile + ", " + PixelShader + "));\n";

			return text + idention + "}";
		}
		#endregion
	}
}
