using System;
using System.Collections.Generic;
using System.IO;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using Dx11 = SharpDX.Direct3D11;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
	public class Effect_Metro : INativeEffect
	{
		#region Private
		private Effect managedEffect;

		private List<EffectTechnique> techniques;
		private List<EffectParameter> parameters;

		private ExtendedShader shader;
		#endregion

		#region Public
		internal Dx11.VertexShader NativeVertexShader
		{
			get;
			private set;
		}

		internal Dx11.PixelShader NativePixelShader
		{
			get;
			private set;
		}

		public IEnumerable<EffectTechnique> Techniques
		{
			get
			{
				if (techniques == null)
				{
					ParseTechniques();
				}

				return techniques;
			}
		}

		public IEnumerable<EffectParameter> Parameters
		{
			get
			{
				if (parameters == null)
				{
					ParseParameters();
				}

				return parameters;
			}
		}
		#endregion

		#region Constructor
		public Effect_Metro(GraphicsDevice device, Effect managedEffect,
			Stream vertexShaderByteCode, Stream pixelShaderByteCode)
		{
			this.managedEffect = managedEffect;

			throw new NotImplementedException();

			/*byte[] vertexData = SeekIfPossibleAndReadBytes(vertexShaderByteCode);
			NativeVertexShader = new Dx11.VertexShader((Dx11.Device)device.NativeDevice, vertexData);

			byte[] pixelData = SeekIfPossibleAndReadBytes(pixelShaderByteCode);
			NativePixelShader = new Dx11.PixelShader((Dx11.Device)device.NativeDevice, pixelData);*/
		}

		public Effect_Metro(GraphicsDevice device, Effect managedEffect, Stream effectByteCode)
		{
			this.managedEffect = managedEffect;

			shader = new ExtendedShader(effectByteCode);
		}
		#endregion

		#region ParseTechniques
		private void ParseTechniques()
		{
			techniques = new List<EffectTechnique>();

			foreach (string key in shader.Techniques.Keys)
			{
				var nativeTechnique = new EffectTechnique_Metro(key, managedEffect, shader.Techniques[key]);
				techniques.Add(new EffectTechnique(managedEffect, nativeTechnique));
			}
		}
		#endregion

		#region ParseParameters
		private void ParseParameters()
		{
			parameters = new List<EffectParameter>();

			foreach (ExtendedShaderParameter parameter in shader.Parameters)
			{
				EffectParameter newParam = new EffectParameter();
				newParam.NativeParameter = new EffectParameter_Metro(parameter);
				parameters.Add(newParam);
			}
		}
		#endregion

		#region SeekIfPossibleAndReadBytes
		private byte[] SeekIfPossibleAndReadBytes(Stream stream)
		{
			if (stream.CanSeek)
			{
				stream.Seek(0, SeekOrigin.Begin);
			}

			int pixelSize = (int)(stream.Length - stream.Position);
			byte[] data = new byte[pixelSize];
			stream.Read(data, 0, pixelSize);

			return data;
		}
		#endregion

		#region Apply
		public void Apply(GraphicsDevice graphicsDevice)
		{
			//TODO: dummy
			((GraphicsDeviceWindowsMetro)graphicsDevice.NativeDevice).currentEffect = this;
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			if (NativeVertexShader != null)
			{
				NativeVertexShader.Dispose();
				NativeVertexShader = null;
			}

			if (NativePixelShader != null)
			{
				NativePixelShader.Dispose();
				NativePixelShader = null;
			}
		}
		#endregion
	}

	public class ExtendedShader
	{
		public Dictionary<string, ExtendedShaderPass[]> Techniques;
		public List<ExtendedShaderParameter> Parameters;

		public ExtendedShader(Stream stream)
		{
			Techniques = new Dictionary<string, ExtendedShaderPass[]>();
			Parameters = new List<ExtendedShaderParameter>();

			BinaryReader reader = new BinaryReader(stream);

			int numberOfVariables = reader.ReadInt32();
			for (int index = 0; index < numberOfVariables; index++)
			{
				Parameters.Add(new ExtendedShaderParameter(reader));
			}

			int numberOfStructures = reader.ReadInt32();
			for (int index = 0; index < numberOfStructures; index++)
			{
				string name = reader.ReadString();
				int numberOfStructVariables = reader.ReadInt32();
				for (int varIndex = 0; varIndex < numberOfStructVariables; varIndex++)
				{
					string varType = reader.ReadString();
					string varName = reader.ReadString();
					string varSemantic = reader.ReadString();
				}
			}

			int numberOfTechniques = reader.ReadInt32();
			for (int index = 0; index < numberOfTechniques; index++)
			{
				string name = reader.ReadString();
				int numberOfPasses = reader.ReadInt32();
				ExtendedShaderPass[] passes = new ExtendedShaderPass[numberOfPasses];
				Techniques.Add(name, passes);

				for (int passIndex = 0; passIndex < numberOfPasses; passIndex++)
				{
					passes[passIndex] = new ExtendedShaderPass(reader);
				}
			}
		}
	}

	public class ExtendedShaderParameter
	{
		public string Type;
		public string Name;

		public ExtendedShaderParameter(BinaryReader reader)
		{
			Type = reader.ReadString();
			Name = reader.ReadString();
		}
	}

	public class ExtendedShaderPass
	{
		public string Name;
		public byte[] VertexCode;
		public byte[] PixelCode;

		public ExtendedShaderPass(BinaryReader reader)
		{
			Name = reader.ReadString();
			int vertexCodeLength = reader.ReadInt32();
			VertexCode = reader.ReadBytes(vertexCodeLength);
			int pixelCodeLength = reader.ReadInt32();
			PixelCode = reader.ReadBytes(pixelCodeLength);
		}
	}
}
