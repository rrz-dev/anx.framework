using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace HLSLParser
{
	public class TypeDef
	{
		#region Constants
		private const string TypedefKeyword = "typedef ";
		#endregion

		#region Public
		public string Value
		{
			get;
			private set;
		}
		#endregion
		
		#region Constructor
		public TypeDef(ParseTextWalker walker)
		{
			string text = walker.Text;
			int indexOfTypedefEnd = text.IndexOf(';');
			Value = text.Substring(0, indexOfTypedefEnd);

			walker.Seek(indexOfTypedefEnd + 1);
		}
		#endregion
		
		#region ParseIfTypeDef
		public static TypeDef ParseIfTypeDef(ParseTextWalker walker)
		{
			if (walker.Text.StartsWith("typedef "))
			{
				walker.Seek(TypedefKeyword.Length);
				return new TypeDef(walker);
			}

			return null;
		}
		#endregion

		#region ToString
		public override string ToString()
		{
			return "typedef " + Value + ";";
		}
		#endregion
	}
}
