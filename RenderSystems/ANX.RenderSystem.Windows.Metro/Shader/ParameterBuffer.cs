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

            nativeBuffer = new Dx11.Buffer(graphicsDevice.NativeDevice, dataSize,
                Dx11.ResourceUsage.Default, Dx11.BindFlags.ConstantBuffer,
                Dx11.CpuAccessFlags.None, Dx11.ResourceOptionFlags.None, 0);
		}
		#endregion

		#region SetParameter (T)
		public void SetParameter<T>(string parameterName, ref T value) where T : struct
		{
			int indexOfParameter = FindParameterIndex(parameterName);
			if (indexOfParameter == -1)
				return;

            int size = Marshal.SizeOf(typeof(T));
            byte[] dataToAdd = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);

            Marshal.StructureToPtr(value, ptr, true);
            Marshal.Copy(ptr, setData, parameterOffsets[indexOfParameter], size);
            Marshal.FreeHGlobal(ptr);
		}
        #endregion

        #region SetParameter (T[])
        public void SetParameter<T>(string parameterName, T[] value) where T : struct
        {
            int indexOfParameter = FindParameterIndex(parameterName);
            if (indexOfParameter == -1)
                return;
            
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

		#region FindParameterIndex
		private int FindParameterIndex(string parameterName)
		{
			int searchIndex = 0;
			foreach (var parameter in parentEffect.shader.Parameters)
			{
				if (parameter.Name == parameterName)
					return searchIndex;

				searchIndex++;
			}

			return -1;
		}
		#endregion
        
		#region Apply
		public void Apply()
		{
            graphicsDevice.NativeContext.VertexShader.SetConstantBuffer(0, nativeBuffer);

            IntPtr dataPtr;
            unsafe
            {
                fixed (byte* ptr = &setData[0])
                    dataPtr = (IntPtr)ptr;
            }

            // Reset really needed? evaluate
            setData = new byte[dataSize];
            var dataBox = new SharpDX.DataBox(dataPtr);
            graphicsDevice.NativeContext.UpdateSubresource(dataBox, nativeBuffer);
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
