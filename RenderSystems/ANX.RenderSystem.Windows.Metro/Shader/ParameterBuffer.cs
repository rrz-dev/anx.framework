using System;
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

        private int[] parameterOffsets;
		private byte[] setData;
		private int dataSize;
		#endregion

		#region Constructor
		public ParameterBuffer(Effect_Metro setParentEffect,
				NativeDxDevice setGraphicsDevice)
		{
			graphicsDevice = setGraphicsDevice;
			parentEffect = setParentEffect;
            dataSize = 0;

            var offsets = new System.Collections.Generic.List<int>();
            foreach(var parameter in parentEffect.shader.Parameters)
            {
                if (parameter.IsTexture == false)
                {
                    offsets.Add(dataSize);
                    dataSize += parameter.SizeInBytes;
                }
            }

            parameterOffsets = offsets.ToArray();
            setData = new byte[dataSize];
		}
		#endregion

		#region SetParameter (T)
		public void SetParameter<T>(string parameterName, T value) where T : struct
		{
			int indexOfParameter = FindParameterIndex(parameterName);
			if (indexOfParameter == -1)
				return;

            byte[] dataToAdd = StructureToBytes(value);
            Array.Copy(dataToAdd, 0, setData, parameterOffsets[indexOfParameter], dataToAdd.Length);
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
            int offset = 0;
            IntPtr ptr = Marshal.AllocHGlobal(sizePerItem);
            for (int index = 0; index < value.Length; index++)
            {
                Marshal.StructureToPtr(value[index], ptr, true);
                Marshal.Copy(ptr, setData, parameterOffsets[indexOfParameter] + offset, sizePerItem);
                offset += sizePerItem;
            }
            Marshal.FreeHGlobal(ptr);
        }
        #endregion

		#region SetParameter (float[])
		public void SetParameter(string parameterName, float[] value)
		{
			int indexOfParameter = FindParameterIndex(parameterName);
			if (indexOfParameter == -1)
				return;

			value = FillArrayIfNeeded(value, indexOfParameter);
			
            byte[] result = UnionArraySerializer.Unify(value);
            Array.Copy(result, 0, setData, parameterOffsets[indexOfParameter], result.Length);
		}
		#endregion

		#region SetParameter (int[])
		public void SetParameter(string parameterName, int[] value)
		{
			int indexOfParameter = FindParameterIndex(parameterName);
			if (indexOfParameter == -1)
				return;

			value = FillArrayIfNeeded(value, indexOfParameter);

            byte[] result = UnionArraySerializer.Unify(value);
            Array.Copy(result, 0, setData, parameterOffsets[indexOfParameter], result.Length);
		}
		#endregion

		#region SetParameter (byte[])
		public void SetParameter(string parameterName, byte[] value)
		{
			int indexOfParameter = FindParameterIndex(parameterName);
			if (indexOfParameter == -1)
				return;

            Array.Copy(value, 0, setData, parameterOffsets[indexOfParameter], value.Length);
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
			IntPtr dataPtr;
			unsafe
			{
				fixed (byte* ptr = &setData[0])
				{
					dataPtr = (IntPtr)ptr;
				}
			}

            // Reset really needed? evaluate
            setData = new byte[dataSize];
			return new SharpDX.DataBox(dataPtr);
		}
		#endregion

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
