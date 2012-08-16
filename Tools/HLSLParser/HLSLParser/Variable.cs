using System;
using System.Collections.Generic;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace HLSLParser
{
	/// <summary>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb509706%28v=vs.85%29.aspx
	/// </summary>
	public class Variable
	{
		#region Constants
		private static char[] SemanticEndChars = new char[]
		{
			' ', ':', '<', ';', '='
		};

		private static readonly List<string> AllTypeModifiers = new List<string>(
			new string[]
			{
				"const",
				"row_major",
				"column_major",
				"extern",
				"nointerpolation",
				"precise",
				"shared",
				"groupshared",
				"static",
				"uniform",
				"volatile",

				// struct additions
				"linear",
				"centroid",
				"noperspective",
				"sample"
			});

		private static readonly List<string> AllTypeNames = new List<string>(
			new string[]
			{
				"int",
				"bool",
				"uint",
				"dword",
				"half",
				"float",
				"double",
				// "min16float",
				// "min10float",
				// "min16int",
				// "min12int",
				// "min16uint",

				// vector <Type, Number>
				"vector",
				// matrix <Type, Rows, Columns>
				"matrix",

				"texture",
				"Texture2D",
			});
		#endregion

		#region Public
		public string[] TypeModifiers
		{
			get;
			private set;
		}

		public string Name
		{
			get;
			private set;
		}

		public string InitialValue
		{
			get;
			private set;
		}

		public int ArraySize
		{
			get;
			private set;
		}

		public string Type
		{
			get;
			private set;
		}

		public string Annotations
		{
			get;
			private set;
		}

		public string Semantic
		{
			get;
			private set;
		}

		public string Packoffset
		{
			get;
			private set;
		}

		public string Register
		{
			get;
			private set;
		}

		public int[] Dimensions
		{
			get;
			private set;
		}
		#endregion
		
		#region Constructor
		protected Variable(ParseTextWalker walker)
		{
			ParseType(walker);
			ReadNameAndArraySize(walker);
			ParseExtraParameters(walker);
			ParseAnnotations(walker);
			ReadInitialValue(walker);

			if (walker.Text.StartsWith(";"))
			{
				walker.Seek(1);
			}
		}
		#endregion

		#region ParseType
		private void ParseType(ParseTextWalker walker)
		{
			string currentText = walker.Text;

			int nextSpaceIndex = FindInitialTypeEndSpace(currentText);
			string variableType = currentText.Substring(0, nextSpaceIndex);
			walker.Seek(nextSpaceIndex + 1);

			variableType = ResolveMatrixAndArraySyntax(variableType);
			ParseDimensions(variableType);
		}
		#endregion

		#region FindInitialTypeEndSpace
		private int FindInitialTypeEndSpace(string text)
		{
			int searchScope = 0;
			if (text.StartsWith("vector") ||
				text.StartsWith("matrix"))
			{
				searchScope = text.IndexOf('>');
			}
			return text.IndexOf(' ', searchScope);
		}
		#endregion

		#region ResolveMatrixAndArraySyntax
		private string ResolveMatrixAndArraySyntax(string text)
		{
			if (text.Contains("<") &&
				text.StartsWith("matrix") ||
				text.StartsWith("vector"))
			{
				text = text.Substring(text.IndexOf('<') + 1);
				text = text.Trim().TrimEnd('>');
				string[] parts = text.Split(',');
				text = parts[0].Trim();
				for (int index = 1; index < parts.Length; index++)
				{
					text += (index > 1 ? "x" : "") + parts[index].Trim();
				}
			}

			return text;
		}
		#endregion

		#region ParseDimensions
		private void ParseDimensions(string typeText)
		{
			List<int> dimensions = new List<int>();
			Type = typeText;
			int numeralRemoverIndex = typeText.Length - 1;
			while (numeralRemoverIndex >= 1)
			{
				if (Char.IsDigit(Type[numeralRemoverIndex]))
				{
					dimensions.Insert(0, int.Parse(Type[numeralRemoverIndex].ToString()));
					Type = Type.Substring(0, numeralRemoverIndex);
				}
				else if (Type[numeralRemoverIndex] == 'x' &&
						Char.IsDigit(Type[numeralRemoverIndex - 1]))
				{
					Type = Type.Substring(0, numeralRemoverIndex);
				}
				else
				{
					break;
				}

				numeralRemoverIndex--;
			}

			Dimensions = dimensions.ToArray();
		}
		#endregion

		#region ReadNameAndArraySize
		private void ReadNameAndArraySize(ParseTextWalker walker)
		{
			string currentText = walker.Text;

			int nameIndex = 0;
			while (nameIndex < currentText.Length)
			{
				char currentChar = currentText[nameIndex];
				if (currentChar == ' ' ||
					currentChar == ':' ||
					currentChar == '<' ||
					currentChar == ';' ||
					currentChar == '=')
				{
					break;
				}
				Name += currentChar;
				nameIndex++;
			}

			if (Name.Contains("["))
			{
				Name = Name.TrimEnd(']');
				int openBraceIndex = Name.IndexOf('[');
				ArraySize = int.Parse(Name.Substring(openBraceIndex + 1));
				Name = Name.Substring(0, openBraceIndex);
			}

			walker.Seek(nameIndex);
		}
		#endregion

		#region ParseExtraParameters
		private void ParseExtraParameters(ParseTextWalker walker)
		{
			string currentText = walker.Text;

			char currentChar = currentText[0];
			if (currentChar == '<' ||
				currentChar == ';' ||
				currentChar == '=')
			{
				return;
			}

			int afterColonIndex = 1;
			string extraText = currentText.Substring(afterColonIndex);

			if (extraText.Trim().StartsWith("packoffset"))
			{
				int endIndex = extraText.IndexOf(')') + 1;
				Packoffset = extraText.Substring(0, endIndex).Trim();
				walker.Seek(endIndex + afterColonIndex);
			}
			else if (extraText.Trim().StartsWith("register"))
			{
				int endIndex = extraText.IndexOf(')') + 1;
				Register = extraText.Substring(0, endIndex).Trim();
				walker.Seek(endIndex + afterColonIndex);
			}
			else
			{
				int beforeLength = extraText.Length;
				extraText = extraText.TrimStart(' ');
				int lowestEndIndex = -1;
				foreach (char semanticEndChar in SemanticEndChars)
				{
					int indexOfEndChar = extraText.IndexOf(semanticEndChar);
					if (indexOfEndChar != -1 &&
						(lowestEndIndex == -1 ||
						indexOfEndChar < lowestEndIndex))
					{
						lowestEndIndex = indexOfEndChar;
					}
				}

				Semantic = extraText.Substring(0, lowestEndIndex).Trim();
				walker.Seek(lowestEndIndex + afterColonIndex + (beforeLength - extraText.Length));
			}
		}
		#endregion

		#region ParseAnnotations
		private void ParseAnnotations(ParseTextWalker walker)
		{
			string currentText = walker.Text;
			if (currentText[0] != '<')
				return;

			int endIndex = currentText.IndexOf('>');
			Annotations = currentText.Substring(1, endIndex - 1);

			walker.Seek(endIndex + 1);
		}
		#endregion

		#region ReadInitialValue
		private void ReadInitialValue(ParseTextWalker walker)
		{
			string currentText = walker.Text;

			int equalSignSearchIndex = 0;
			while (equalSignSearchIndex < currentText.Length)
			{
				char currentChar = currentText[equalSignSearchIndex];
				if (currentChar == '<' ||
					currentChar == ';' ||
					currentChar == ':')
				{
					return;
				}

				if (currentChar == '=')
					break;

				equalSignSearchIndex++;
			}

			int afterEqualSignIndex = equalSignSearchIndex + 1;
			int valueEndIndex = currentText.IndexOf(';', afterEqualSignIndex);
			InitialValue = currentText.Substring(afterEqualSignIndex,
				valueEndIndex - afterEqualSignIndex);
			InitialValue = InitialValue.Trim();
		}
		#endregion

		#region GetTypeModifiers
		public static string[] GetTypeModifiers(ParseTextWalker walker)
		{
			if (walker == null)
				return new string[0];

			if (String.IsNullOrEmpty(walker.Text))
				return new string[0];

			string currentText = walker.Text;

			int firstSpaceIndex = currentText.IndexOf(' ');
			if (firstSpaceIndex == -1)
			{
				return AllTypeModifiers.Contains(currentText) ?
					new string[] { currentText } :
					new string[0];
			}

			var result = new List<string>();
			while (firstSpaceIndex != -1)
			{
				string currentElement = currentText.Substring(0, firstSpaceIndex);

				if (AllTypeModifiers.Contains(currentElement))
				{
					result.Add(currentElement);
				}
				else
				{
					break;
				}

				walker.Seek(firstSpaceIndex + 1);
				currentText = walker.Text;
				firstSpaceIndex = currentText.IndexOf(' ');
			}

			return result.ToArray();
		}
		#endregion

		#region IsVariableFollowing
		public static bool IsVariableFollowing(ParseTextWalker walker)
		{
			string currentText = walker.Text;

			foreach (string typeName in AllTypeNames)
			{
				if (currentText.StartsWith(typeName))
					return true;
			}

			return false;
		}
		#endregion
		
		#region ParseIfVariable
		public static Variable ParseIfVariable(ParseTextWalker walker)
		{
			string[] typeModifiersFound = GetTypeModifiers(walker);

			if (IsVariableFollowing(walker))
			{
				return new Variable(walker)
				{
					TypeModifiers = typeModifiersFound,
				};
			}
			else
				return null;
		}
		#endregion

		#region ToString
		public override string ToString()
		{
			string result = "";
			result = AddTypeModifiersToString(result);
			result += Type;
			result = AddDimensionsToString(result);
			result += " " + Name;

			if (ArraySize > 0)
			{
				result += "[" + ArraySize + "]";
			}

			if (String.IsNullOrEmpty(Semantic) == false)
			{
				result += " : " + Semantic;
			}

			if (String.IsNullOrEmpty(Packoffset) == false)
			{
				result += " : " + Packoffset;
			}

			if (String.IsNullOrEmpty(Register) == false)
			{
				result += " : " + Register;
			}

			if (String.IsNullOrEmpty(Annotations) == false)
			{
				result += " <" + Annotations + ">";
			}

			if (String.IsNullOrEmpty(InitialValue) == false)
			{
				result += " = " + InitialValue;
			}

			result += ";";

			return result;
		}
		#endregion

		#region AddTypeModifiersToString
		private string AddTypeModifiersToString(string text)
		{
			foreach (string modifier in TypeModifiers)
			{
				text += modifier + " ";
			}

			return text;
		}
		#endregion

		#region AddDimensionsToString
		private string AddDimensionsToString(string text)
		{
			for (int index = 0; index < Dimensions.Length; index++)
			{
				text += (index > 0 ? "x" : "") + Dimensions[index];
			}

			return text;
		}
		#endregion
	}
}
