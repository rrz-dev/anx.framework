using System;

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

		public string PixelShader
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
			int indexOfVertexShaderStart = text.IndexOf("SetVertexShader(") +
				"SetVertexShader(".Length;
			int indexOfVertexShaderEnd = text.IndexOf(");", indexOfVertexShaderStart);
			VertexShader = text.Substring(indexOfVertexShaderStart,
				indexOfVertexShaderEnd - indexOfVertexShaderStart);

			int indexOfPixelShaderStart = text.IndexOf("SetPixelShader(") +
				"SetPixelShader(".Length;
			int indexOfPixelShaderEnd = text.IndexOf(");", indexOfPixelShaderStart);
			PixelShader = text.Substring(indexOfPixelShaderStart,
				indexOfPixelShaderEnd - indexOfPixelShaderStart);
		}
		#endregion
		
		#region ParseDx9Pass
		private void ParseDx9Pass(string text)
		{
			int indexOfVertexShaderStart = text.IndexOf("VertexShader");
			indexOfVertexShaderStart = text.IndexOf("compile ", indexOfVertexShaderStart);
			int indexOfVertexShaderEnd = text.IndexOf(';', indexOfVertexShaderStart);
			VertexShader = text.Substring(indexOfVertexShaderStart,
				indexOfVertexShaderEnd - indexOfVertexShaderStart);
			VertexShader = VertexShader.Replace(" (", "(");
			VertexShader = VertexShader.Replace("( ", "(");
			VertexShader = VertexShader.Replace(" )", ")");
			VertexShader = VertexShader.Replace(") ", ")");

			int indexOfPixelShaderStart = text.IndexOf("PixelShader");
			indexOfPixelShaderStart = text.IndexOf("compile ", indexOfPixelShaderStart);
			int indexOfPixelShaderEnd = text.IndexOf(';', indexOfPixelShaderStart);
			PixelShader = text.Substring(indexOfPixelShaderStart,
				indexOfPixelShaderEnd - indexOfPixelShaderStart);
			PixelShader = PixelShader.Replace(" (", "(");
			PixelShader = PixelShader.Replace("( ", "(");
			PixelShader = PixelShader.Replace(" )", ")");
			PixelShader = PixelShader.Replace(") ", ")");
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

			text += idention + "\tSetVertexShader(" + VertexShader + ");\n";
			text += idention + "\tSetGeometryShader(NULL);\n";
			text += idention + "\tSetPixelShader(" + PixelShader + ");\n";

			return text + idention + "}";
		}
		#endregion
	}
}
