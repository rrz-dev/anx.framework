using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace HLSLParser
{
	public class Pass
	{
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
			get;
			private set;
		}

		public string PixelShader
		{
			get;
			private set;
		}

		public string PixelShaderProfile
		{
			get;
			private set;
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
			int subPartStartIndex = "CompileShader(".Length;

			int indexOfVertexShaderStart = text.IndexOf("SetVertexShader(") +
				"SetVertexShader(".Length;
			int indexOfVertexShaderEnd = text.IndexOf(");", indexOfVertexShaderStart);
			VertexShader = text.Substring(indexOfVertexShaderStart,
				indexOfVertexShaderEnd - indexOfVertexShaderStart);
			VertexShader = VertexShader.Replace(" ", "");
			VertexShader = VertexShader.Replace("\t", "");
			VertexShader = VertexShader.Replace("\n", "");
			VertexShader = VertexShader.Replace("\r", "");
			VertexShader = VertexShader.Substring(subPartStartIndex,
				VertexShader.Length - subPartStartIndex - 1);
			int indexOfFirstComma = VertexShader.IndexOf(',');
			VertexShaderProfile = VertexShader.Substring(0, indexOfFirstComma);
			VertexShader = VertexShader.Substring(indexOfFirstComma + 1);
			VertexShader = VertexShader.Replace(",", ", ");

			int indexOfPixelShaderStart = text.IndexOf("SetPixelShader(") +
				"SetPixelShader(".Length;
			int indexOfPixelShaderEnd = text.IndexOf(");", indexOfPixelShaderStart);
			PixelShader = text.Substring(indexOfPixelShaderStart,
				indexOfPixelShaderEnd - indexOfPixelShaderStart);
			PixelShader = PixelShader.Replace(" ", "");
			PixelShader = PixelShader.Replace("\t", "");
			PixelShader = PixelShader.Replace("\n", "");
			PixelShader = PixelShader.Replace("\r", "");
			PixelShader = PixelShader.Substring(subPartStartIndex,
				PixelShader.Length - subPartStartIndex - 1);
			indexOfFirstComma = PixelShader.IndexOf(',');
			PixelShaderProfile = PixelShader.Substring(0, indexOfFirstComma);
			PixelShader = PixelShader.Substring(indexOfFirstComma + 1);
			PixelShader = PixelShader.Replace(",", ", ");
		}
		#endregion
		
		#region ParseDx9Pass
		private void ParseDx9Pass(string text)
		{
			VertexShader = ParseDx9Shader(text, text.IndexOf("VertexShader"));
			int indexOfSpaceAfterProfile = VertexShader.IndexOf(' ');
			VertexShaderProfile = VertexShader.Substring(0, indexOfSpaceAfterProfile);
			VertexShader = VertexShader.Substring(indexOfSpaceAfterProfile).Trim();

			PixelShader = ParseDx9Shader(text, text.IndexOf("PixelShader"));
			indexOfSpaceAfterProfile = PixelShader.IndexOf(' ');
			PixelShaderProfile = PixelShader.Substring(0, indexOfSpaceAfterProfile);
			PixelShader = PixelShader.Substring(indexOfSpaceAfterProfile).Trim();
		}
		#endregion

		#region ParseDx9Shader
		private string ParseDx9Shader(string text, int indexOfShaderStart)
		{
			string shader = "";
			indexOfShaderStart = text.IndexOf("compile ", indexOfShaderStart);
			int indexOfShaderEnd = text.IndexOf(';', indexOfShaderStart);
			shader = text.Substring(indexOfShaderStart,
				indexOfShaderEnd - indexOfShaderStart);
			shader = shader.Replace(" (", "(");
			shader = shader.Replace("( ", "(");
			shader = shader.Replace(" )", ")");
			shader = shader.Replace(") ", ")");
			shader = shader.Replace("compile", "");
			shader = shader.Trim();
			return shader;
		}
		#endregion

		#region ParseIfPass
		public static Pass ParseIfPass(ParseTextWalker walker)
		{
			if (walker.Text.StartsWith("pass "))
			{
				return new Pass(walker);
			}

			return null;
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
