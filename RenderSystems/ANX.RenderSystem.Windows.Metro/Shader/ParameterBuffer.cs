using SharpDX;
using System;
using System.Collections.Generic;
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

            //constant buffers must be aligned to 16 byte
            dataSize = (dataSize + 15) / 16 * 16;

            parameterOffsets = offsets.ToArray();
            setData = new byte[dataSize];

            var description = new Dx11.BufferDescription()
            {
                BindFlags = Dx11.BindFlags.ConstantBuffer,
                CpuAccessFlags = Dx11.CpuAccessFlags.Write,
                Usage = Dx11.ResourceUsage.Dynamic,
                SizeInBytes = dataSize,
            };
            
            nativeBuffer = new Dx11.Buffer(graphicsDevice.NativeDevice, description);
        }
        #endregion

        #region SetParameter (T)
        public void SetParameter<T>(string parameterName, ref T value) where T : struct
        {
            int indexOfParameter = FindParameterIndex(parameterName);
            if (indexOfParameter == -1)
                throw new KeyNotFoundException(string.Format("No parameter with name \"{1}\" found.", parameterName));

            int size = Marshal.SizeOf(typeof(T));
            IntPtr ptr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(value, ptr, true);
                Marshal.Copy(ptr, setData, parameterOffsets[indexOfParameter], size);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
        #endregion

        #region SetParameter (T[])
        public void SetParameter<T>(string parameterName, T[] value) where T : struct
        {
            if (value == null)
                throw new ArgumentNullException("value");

            int indexOfParameter = FindParameterIndex(parameterName);
            if (indexOfParameter == -1)
                throw new KeyNotFoundException(string.Format("No parameter with name \"{1}\" found.", parameterName));

            var handle = GCHandle.Alloc(value, GCHandleType.Pinned);
            try
            {
                Marshal.Copy(handle.AddrOfPinnedObject(), setData, parameterOffsets[indexOfParameter], Marshal.SizeOf(typeof(T)) * value.Length);
            }
            finally
            {
                handle.Free();
            }
        }
        #endregion

        #region FindParameterIndex
        private int FindParameterIndex(string parameterName)
        {
            int searchIndex = 0;
            foreach (var parameter in parentEffect.shader.Parameters)
            {
                if (parameter.IsTexture)
                    continue;

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
            DataStream stream;
            graphicsDevice.NativeContext.MapSubresource(this.nativeBuffer, Dx11.MapMode.WriteDiscard, Dx11.MapFlags.None, out stream);
            stream.WriteRange(setData);
            graphicsDevice.NativeContext.UnmapSubresource(this.nativeBuffer, 0);

            graphicsDevice.NativeContext.VertexShader.SetConstantBuffer(0, nativeBuffer);
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
