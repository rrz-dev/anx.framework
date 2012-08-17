using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace HLSLParser
{
	public class TypeDef : IShaderElement
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
			int indexOfTypedefEnd = walker.Text.IndexOf(';');
			Value = walker.Text.Substring(0, indexOfTypedefEnd);

			walker.Seek(indexOfTypedefEnd + 1);
		}
		#endregion

		#region TryParse
		public static TypeDef TryParse(ParseTextWalker walker)
		{
			if (walker.Text.StartsWith(TypedefKeyword))
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
			return TypedefKeyword + Value + ";";
		}
		#endregion
	}
}
