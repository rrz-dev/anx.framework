using System;
using System.Collections.Generic;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Helpers.HLSLParser
{
	public class Structure : IShaderElement
	{
		#region Public
		public string Name
		{
			get;
			private set;
		}

		public List<Variable> Variables
		{
			get;
			private set;
		}
		#endregion
		
		#region Constructor
		public Structure(ParseTextWalker walker)
		{
			Variables = new List<Variable>();

			string currentText = walker.Text;

			int indexOfStructOpenBrace = currentText.IndexOf('{');
			walker.Seek(indexOfStructOpenBrace + 1);

			Name = currentText.Substring(0, indexOfStructOpenBrace);
			Name = Name.Replace("struct ", "").Trim();

			Variable newVariable = null;
			while ((newVariable = Variable.TryParse(walker)) != null)
			{
				Variables.Add(newVariable);
			}

			currentText = walker.Text;
			int indexOfStructCloseBrace = currentText.IndexOf("};");
			walker.Seek(indexOfStructCloseBrace + 2);
		}
		#endregion

		#region TryParse
		public static Structure TryParse(ParseTextWalker walker)
		{
			string currentText = walker.Text;
			if (currentText.StartsWith("struct"))
			{
				return new Structure(walker);
			}

			return null;
		}
		#endregion

		#region ToString
		public override string ToString()
		{
			string result = "struct " + Name + "\n{";

			foreach (Variable variable in Variables)
			{
				result += "\n\t" + variable.ToString();
			}

			return result + "\n};";
		}
		#endregion
	}
}
