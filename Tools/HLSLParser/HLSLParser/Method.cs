using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace HLSLParser
{
	public class Method : IShaderElement
	{
		#region Public
		public string StorageClass
		{
			get;
			private set;
		}

		public string ReturnType
		{
			get;
			private set;
		}

		public string Name
		{
			get;
			private set;
		}

		public string Semantic
		{
			get;
			private set;
		}

		public string Arguments
		{
			get;
			private set;
		}

		public string Body
		{
			get;
			private set;
		}
		#endregion

		#region Constructor
		public Method(ParseTextWalker walker)
		{
			string text = walker.Text;
			if (text.StartsWith("inline"))
			{
				StorageClass = "inline";
				walker.Seek("inline".Length);
				text = walker.Text;
			}

			int indexOfOpenParenthesis = text.IndexOf('(');
			int indexOfCloseParenthesis = text.IndexOf(')');

			string headerPart = text.Substring(0, indexOfOpenParenthesis).Trim();
			headerPart = walker.RemoveUnneededChars(headerPart);

			string[] parts = headerPart.Split(new char[] { ' ' },
				StringSplitOptions.RemoveEmptyEntries);

			ReturnType = parts[0].Trim();
			Name = parts[1].Trim();

			Arguments = text.Substring(indexOfOpenParenthesis + 1,
				indexOfCloseParenthesis - indexOfOpenParenthesis - 1);

			walker.Seek(indexOfCloseParenthesis + 1);
			text = walker.Text;

			int indexOfMethodBodyStart = text.IndexOf('{');

			if (text.StartsWith(":"))
			{
				Semantic = text.Substring(1, indexOfMethodBodyStart - 1).Trim();
				Semantic = walker.RemoveUnneededChars(Semantic);
			}

			walker.Seek(indexOfMethodBodyStart + 1);

			GetMethodBody(walker);
		}
		#endregion

		#region GetMethodBody
		private void GetMethodBody(ParseTextWalker walker)
		{
			string text = walker.Text;

			int numberOfOpenBraces = 0;
			int searchBodyEndIndex = 0;
			while (searchBodyEndIndex < text.Length)
			{
				char currentChar = text[searchBodyEndIndex];
				if (currentChar == '{')
					numberOfOpenBraces++;
				else if (currentChar == '}')
					numberOfOpenBraces--;

				if (numberOfOpenBraces == -1)
					break;

				searchBodyEndIndex++;
			}

			Body = text.Substring(0, searchBodyEndIndex - 1);
			Body = Body.TrimEnd('\n', '\r', '\t');

			walker.Seek(searchBodyEndIndex + 1);
		}
		#endregion

		#region TryParse
		public static Method TryParse(ParseTextWalker walker)
		{
			if(walker.Text.StartsWith("inline ") ||
				CheckIfMethodHeaderExists(walker))
			{
				return new Method(walker);
			}

			return null;
		}
		#endregion

		#region CheckIfMethodHeaderExists
		private static bool CheckIfMethodHeaderExists(ParseTextWalker walker)
		{
			string text = walker.Text;
			int indexOfOpenParenthesis = text.IndexOf('(');
			if (indexOfOpenParenthesis == -1)
				return false;

			string headerPart = text.Substring(0, indexOfOpenParenthesis).Trim();
			headerPart = headerPart.Replace("\n", "");
			headerPart = headerPart.Replace("\r", "");
			headerPart = headerPart.Replace("\t", "");

			string[] parts = headerPart.Split(new char[] { ' ' },
				StringSplitOptions.RemoveEmptyEntries);

			return parts.Length == 2;
		}
		#endregion

		#region ToString
		public override string ToString()
		{
			string result = "";
			if (String.IsNullOrEmpty(StorageClass) == false)
			{
				result += StorageClass + " ";
			}

			result += ReturnType + " " + Name + "(" + Arguments + ")";

			if (String.IsNullOrEmpty(Semantic) == false)
			{
				result += " : " + Semantic;
			}

			result += "\n{\n\t" + Body + "\n}";

			return result;
		}
		#endregion
	}
}
