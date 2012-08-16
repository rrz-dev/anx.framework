using System;
using System.Collections.Generic;
using System.IO;
using HLSLParser;
using SharpDX.D3DCompiler;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace DX11MetroShaderGenerator
{
	public class MetroCodeGenerator
	{
		#region Private
		private EffectFile sourceEffect;

		private string resultSourceCode;

		private Dictionary<string, CompiledPass[]> compiledTechniques;
		#endregion

		#region Public
		public byte[] ResultByteCode
		{
			get;
			private set;
		}
		#endregion

		#region Constructor
		public MetroCodeGenerator(string sourceCode)
		{
			Parser parser = Parser.LoadFromSource(sourceCode);
			parser.Parse();

			sourceEffect = parser.Effect;
			BuildSourceCode();

			compiledTechniques = new Dictionary<string, CompiledPass[]>();
			CompileTechniques();

			ExportToByteCode();
		}
		#endregion

		#region BuildSourceCode
		private void BuildSourceCode()
		{
			resultSourceCode = "";
			foreach (var typeDef in sourceEffect.TypeDefs)
			{
				resultSourceCode += typeDef.ToString() + "\n";
			}
			foreach (var variable in sourceEffect.Variables)
			{
				resultSourceCode += variable.ToString() + "\n";
			}
			foreach (var sampler in sourceEffect.Samplers)
			{
				resultSourceCode += sampler.ToString() + "\n";
			}
			foreach (var structure in sourceEffect.Structures)
			{
				resultSourceCode += structure.ToString() + "\n";
			}
			foreach (var buffer in sourceEffect.Buffers)
			{
				resultSourceCode += buffer.ToString() + "\n";
			}
			foreach (var method in sourceEffect.Methods)
			{
				resultSourceCode += method.ToString() + "\n";
			}
		}
		#endregion

		#region CompileTechniques
		private void CompileTechniques()
		{
			foreach (Technique technique in sourceEffect.Techniques)
			{
				CompiledPass[] passes = new CompiledPass[technique.Passes.Count];
				compiledTechniques.Add(technique.Name, passes);

				int passIndex = 0;
				foreach (Pass pass in technique.Passes)
				{
					passes[passIndex] = CompilePass(pass);
				}
			}
		}
		#endregion

		#region CompilePass
		private CompiledPass CompilePass(Pass pass)
		{
			byte[] vertexCode = CompileShader(pass.VertexShaderProfile, pass.VertexShader);
			byte[] pixelCode = CompileShader(pass.PixelShaderProfile, pass.PixelShader);

			return new CompiledPass(vertexCode, pixelCode);
		}
		#endregion

		#region CompileShader
		private byte[] CompileShader(string profile, string entryPoint)
		{
			int indexOfOpenParenthesis = entryPoint.IndexOf('(');
			entryPoint = entryPoint.Substring(0, indexOfOpenParenthesis);

			ShaderBytecode byteCode = ShaderBytecode.Compile(resultSourceCode,
				entryPoint, profile, ShaderFlags.None, EffectFlags.None,
				null, new IncludeHandler(""));

			byte[] result = new byte[byteCode.BufferSize];
			byteCode.Data.Read(result, 0, result.Length);
			return result;
		}
		#endregion

		#region ExportToByteCode
		private void ExportToByteCode()
		{
			using (MemoryStream stream = new MemoryStream())
			{
				BinaryWriter writer = new BinaryWriter(stream);

				ExportVariables(writer);
				ExportStructures(writer);
				ExportTechniques(writer);

				ResultByteCode = stream.ToArray();
			}
		}
		#endregion

		#region ExportVariables
		private void ExportVariables(BinaryWriter writer)
		{
			writer.Write(sourceEffect.Variables.Count);
			foreach (Variable variable in sourceEffect.Variables)
			{
				writer.Write(variable.Type);
				writer.Write(variable.Name);
			}
		}
		#endregion

		#region ExportStructures
		private void ExportStructures(BinaryWriter writer)
		{
			writer.Write(sourceEffect.Structures.Count);
			foreach (Structure structure in sourceEffect.Structures)
			{
				writer.Write(structure.Name);
				writer.Write(structure.Variables.Count);
				foreach (Variable variable in structure.Variables)
				{
					writer.Write(variable.Type);
					writer.Write(variable.Name);
					writer.Write(variable.Semantic);
				}
			}
		}
		#endregion

		#region ExportTechniques
		private void ExportTechniques(BinaryWriter writer)
		{
			writer.Write(compiledTechniques.Count);
			foreach (string key in compiledTechniques.Keys)
			{
				CompiledPass[] passes = compiledTechniques[key];
				writer.Write(key);
				writer.Write(passes.Length);
				foreach (CompiledPass pass in passes)
				{
					writer.Write(pass.VertexShaderCode.Length);
					writer.Write(pass.VertexShaderCode);
					writer.Write(pass.PixelShaderCode.Length);
					writer.Write(pass.PixelShaderCode);
				}
			}
		}
		#endregion
	}
}
