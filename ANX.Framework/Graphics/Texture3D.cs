#region Using Statements
using System;
using System.Runtime.InteropServices;
using ANX.Framework.NonXNA.Development;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	[PercentageComplete(0)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
	public class Texture3D : Texture, IGraphicsResource
	{
		public int Depth
		{
			get;
			private set;
		}

		public int Width
		{
			get;
			private set;
		}

		public int Height
		{
			get;
			private set;
		}

		public Texture3D(GraphicsDevice graphics, int width, int height, int depth,
			[MarshalAsAttribute(UnmanagedType.U1)] bool mipMap, SurfaceFormat format)
			: base(graphics)
		{
		}

		public void GetData<T>(int level, int left, int top, int right, int bottom, int front, int back, T[] data,
			int startIndex, int elementCount) where T : struct
		{
			throw new NotImplementedException();
		}

		public void GetData<T>(T[] data) where T : struct
		{
			throw new NotImplementedException();
		}

		public void GetData<T>(T[] data, int startIndex, int elementCount) where T : struct
		{
			throw new NotImplementedException();
		}

		public void SetData<T>(int level, int left, int top, int right, int bottom, int front, int back, T[] data,
			int startIndex, int elementCount) where T : struct
		{
			throw new NotImplementedException();
		}

		public void SetData<T>(T[] data) where T : struct
		{
			throw new NotImplementedException();
		}

		public void SetData<T>(T[] data, int startIndex, int elementCount) where T : struct
		{
			throw new NotImplementedException();
		}

		protected override void Dispose(bool disposeManaged)
		{
			throw new NotImplementedException();
		}

		internal override void ReCreateNativeTextureSurface()
		{
			throw new NotImplementedException();
		}
	}
}
