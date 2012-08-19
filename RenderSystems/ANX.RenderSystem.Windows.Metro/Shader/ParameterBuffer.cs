using System;
using System.IO;
using System.Runtime.InteropServices;
using Dx11 = SharpDX.Direct3D11;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro.Shader
{
	public class ParameterBuffer : IDisposable
	{
		#region Private
		private Dx11.Buffer nativeBuffer;
		private NativeDxDevice graphicsDevice;
		private Effect_Metro parentEffect;

		private Array[] setData;

		private int dataSize;
		#endregion

		#region Constructor
		public ParameterBuffer(Effect_Metro setParentEffect,
				NativeDxDevice setGraphicsDevice)
		{
			graphicsDevice = setGraphicsDevice;
			parentEffect = setParentEffect;
			setData = new Array[parentEffect.shader.Parameters.Count];
		}
		#endregion

		#region SetParameter (T)
		public void SetParameter<T>(string parameterName, T value) where T : struct
		{
			int indexOfParameter = FindParameterIndex(parameterName);
			if (indexOfParameter == -1)
				return;

			setData[indexOfParameter] = StructureToBytes(value);
		}
        #endregion

        #region SetParameter (T[])
        public void SetParameter<T>(string parameterName, T[] value) where T : struct
        {
            int indexOfParameter = FindParameterIndex(parameterName);
            if (indexOfParameter == -1)
                return;

            value = FillArrayIfNeeded(value, indexOfParameter);

            int sizePerItem = Marshal.SizeOf(typeof(T));
            byte[] result = new byte[sizePerItem * value.Length];
            int offset = 0;
            IntPtr ptr = Marshal.AllocHGlobal(sizePerItem);
            for (int index = 0; index < value.Length; index++)
            {
                Marshal.StructureToPtr(value[index], ptr, true);
                Marshal.Copy(ptr, result, offset, sizePerItem);
                offset += sizePerItem;
            }
            Marshal.FreeHGlobal(ptr);

            setData[indexOfParameter] = result;
        }
        #endregion

		#region SetParameter (float[])
		public void SetParameter(string parameterName, float[] value)
		{
			int indexOfParameter = FindParameterIndex(parameterName);
			if (indexOfParameter == -1)
				return;

			value = FillArrayIfNeeded(value, indexOfParameter);
			
            byte[] convertData = null;
			byte[] result = UnionArraySerializer.Unify(value);
			convertData = new byte[result.Length];
			Array.Copy(result, convertData, result.Length);

			setData[indexOfParameter] = convertData;
		}
		#endregion

		#region SetParameter (int[])
		public void SetParameter(string parameterName, int[] value)
		{
			int indexOfParameter = FindParameterIndex(parameterName);
			if (indexOfParameter == -1)
				return;

			value = FillArrayIfNeeded(value, indexOfParameter);

			byte[] convertData = null;
			byte[] result = UnionArraySerializer.Unify(value);
			convertData = new byte[result.Length];
			Array.Copy(result, convertData, result.Length);

			setData[indexOfParameter] = convertData;
		}
		#endregion

		#region SetParameter (byte[])
		public void SetParameter(string parameterName, byte[] value)
		{
			int indexOfParameter = FindParameterIndex(parameterName);
			if (indexOfParameter == -1)
				return;

			setData[indexOfParameter] = value;
		}
		#endregion

		#region StructureToBytes
		private byte[] StructureToBytes<T>(T value) where T : struct
		{
			int size = Marshal.SizeOf(value);
			byte[] result = new byte[size];
			IntPtr ptr = Marshal.AllocHGlobal(size);

			Marshal.StructureToPtr(value, ptr, true);
			Marshal.Copy(ptr, result, 0, size);
			Marshal.FreeHGlobal(ptr);

			return result;
		}
		#endregion

		#region FindParameterIndex
		private int FindParameterIndex(string parameterName)
		{
			int searchIndex = 0;
			foreach (var parameter in parentEffect.shader.Parameters)
			{
				if (parameter.Name == parameterName)
				{
					return searchIndex;
				}

				searchIndex++;
			}

			return -1;
		}
		#endregion

		#region FillArrayIfNeeded
		private T[] FillArrayIfNeeded<T>(T[] original, int parameterIndex) where T : struct
        {
            int paramArraySize = parentEffect.shader.Parameters[parameterIndex].ArraySize;
            if (paramArraySize > 0)
			{
                T[] filledArray = new T[paramArraySize];
				Array.Copy(original, filledArray, original.Length);
				return filledArray;
			}

			return original;
		}
		#endregion

		#region Apply
		public void Apply()
		{
			var data = CreateBufferData();

			nativeBuffer = new Dx11.Buffer(graphicsDevice.NativeDevice, dataSize,
					Dx11.ResourceUsage.Default, Dx11.BindFlags.ConstantBuffer,
					Dx11.CpuAccessFlags.None, Dx11.ResourceOptionFlags.None, 0);
			graphicsDevice.NativeContext.VertexShader.SetConstantBuffer(0, nativeBuffer);
			graphicsDevice.NativeContext.UpdateSubresource(data, nativeBuffer);
		}
		#endregion

		#region CreateBufferData
		private SharpDX.DataBox CreateBufferData()
		{
			MemoryStream stream = new MemoryStream();
			BinaryWriter writer = new BinaryWriter(stream);

            for(int index = 0; index < setData.Length; index++)
            {
                byte[] data = (byte[])setData[index];
                if (data != null)
                {
                    writer.Write(data);
                }
                else
                {
                    var parameter = parentEffect.shader.Parameters[index];
                    if (parameter.Type.ToLower().Contains("texture"))
                        continue;

                    int size = GetParameterTypeSize(parameter.Type);
                    foreach (int dimension in parameter.TypeDimensions)
                    {
                        size *= dimension;
                    }

                    size *= parameter.ArraySize;
                    writer.Write(new byte[size]);
                }
			}

			byte[] streamBytes = stream.ToArray();
			stream.Dispose();

			IntPtr dataPtr;
			unsafe
			{
				fixed (byte* ptr = &streamBytes[0])
				{
					dataPtr = (IntPtr)ptr;
				}
			}

			dataSize = streamBytes.Length;

			setData = new Array[parentEffect.shader.Parameters.Count];
			return new SharpDX.DataBox(dataPtr);
		}
		#endregion

        private int GetParameterTypeSize(string type)
        {
            if (type == "float" ||
                type == "int" ||
                type == "uint" ||
                type == "dword")
                return 4;
            if (type == "double")
                return 8;
            if (type == "bool")
                return 1;
            if (type == "half")
                return 2;

            throw new NotImplementedException("Parameter type " + type + " has no size value!");
        }

		#region Dispose
		public void Dispose()
		{
			if (nativeBuffer != null)
			{
				nativeBuffer.Dispose();
				nativeBuffer = null;
			}

			graphicsDevice = null;
		}
		#endregion
	}
}
