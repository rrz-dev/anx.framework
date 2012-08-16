using System;
using System.IO;

namespace HLSLParser
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

		#region Load
		public void Load(string filepath)
		{
			ValidateFilepath(filepath);

			Effect = new EffectFile(filepath);
		}
		#endregion

		#region Parse
		public void Parse()
		{
			Effect.Result = CommentRemover.Remove(Effect.Source);

			WalkText();
		}
		#endregion

		#region WalkText
		private void WalkText()
		{
			var textWalker = new ParseTextWalker(Effect.Result);

			while (textWalker.Text.Length > 0)
			{
				int beforeLength = textWalker.Text.Length;

				var newMethod = Method.ParseIfMethod(textWalker);
				if (newMethod != null)
				{
					Effect.Methods.Add(newMethod);
					continue;
				}

				var newStruct = Structure.ParseIfStructure(textWalker);
				if (newStruct != null)
				{
					Effect.Structures.Add(newStruct);
					continue;
				}

				var newTypeDef = TypeDef.ParseIfTypeDef(textWalker);
				if (newTypeDef != null)
				{
					Effect.TypeDefs.Add(newTypeDef);
					continue;
				}

				var result = Sampler.ParseIfSampler(textWalker);
				if (result != null)
				{
					Effect.Samplers.Add(result);
					continue;
				}

				var newTechnique = Technique.ParseIfTechnique(textWalker);
				if (newTechnique != null)
				{
					Effect.Techniques.Add(newTechnique);
					continue;
				}

				var newBuffer = EffectBuffer.ParseIfBuffer(textWalker);
				if (newBuffer != null)
				{
					Effect.Buffers.Add(newBuffer);
					continue;
				}

				var newVariable = Variable.ParseIfVariable(textWalker);
				if (newVariable != null)
				{
					Effect.Variables.Add(newVariable);
					continue;
				}

				if (textWalker.Text.Length == beforeLength)
					textWalker.Seek(1);
			}
		}
		#endregion

		#region ValidateFilepath
		private void ValidateFilepath(string filepath)
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
