using System;
using System.Collections.Generic;
using System.IO;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Helpers.HLSLParser
{
	/// <summary>
	/// Spec:
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb509615%28v=vs.85%29.aspx
	/// </summary>
	public class Parser
	{
		#region Public
		public EffectFile Effect
		{
			get;
			private set;
		}
		#endregion

		#region Constructor
		private Parser(string sourceCode)
		{
			Effect = new EffectFile(sourceCode);
		}
		#endregion
		
		#region LoadFromFile
		public static Parser LoadFromFile(string filepath)
		{
			ValidateFilepath(filepath);
			return new Parser(File.ReadAllText(filepath));
		}
		#endregion

		#region LoadFromSource
		public static Parser LoadFromSource(string sourceCode)
		{
			return new Parser(sourceCode);
		}
		#endregion

		#region Parse
		public void Parse()
		{
			string commentFreeSource = CommentRemover.Remove(Effect.Source);
			var textWalker = new ParseTextWalker(commentFreeSource);
			
			WalkText(textWalker);
		}
		#endregion

		#region WalkText
		private void WalkText(ParseTextWalker walker)
		{
			while (walker.Text.Length > 0)
			{
				int beforeLength = walker.Text.Length;

				if (TryParse(Method.TryParse(walker), Effect.Methods))
					continue;

				if (TryParse(Structure.TryParse(walker), Effect.Structures))
					continue;

				if (TryParse(TypeDef.TryParse(walker), Effect.TypeDefs))
					continue;

				if (TryParse(Sampler.TryParse(walker), Effect.Samplers))
					continue;

				if (TryParse(Technique.TryParse(walker), Effect.Techniques))
					continue;

				if (TryParse(EffectBuffer.TryParse(walker), Effect.Buffers))
					continue;

				if (TryParse(Variable.TryParse(walker), Effect.Variables))
					continue;

				if (walker.Text.Length == beforeLength)
					walker.Seek(1);
			}
		}
		#endregion

		#region TryParse
		private bool TryParse<T>(IShaderElement parsedElement, List<T> addList)
			where T : IShaderElement
		{
			if (parsedElement != null)
			{
				addList.Add((T)parsedElement);
				return true;
			}

			return false;
		}
		#endregion

		#region ValidateFilepath
		private static void ValidateFilepath(string filepath)
		{
			if (String.IsNullOrEmpty(filepath))
			{
				throw new ArgumentException("filepath");
			}

			if (File.Exists(filepath) == false)
			{
				throw new FileNotFoundException(
					"Unable to load missing file '" + filepath + "'!");
			}
		}
		#endregion
	}
}
