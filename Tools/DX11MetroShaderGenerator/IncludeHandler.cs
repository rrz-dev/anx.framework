using System;
using System.IO;
using SharpDX.D3DCompiler;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace DX11MetroShaderGenerator
{
	internal class IncludeHandler : Include
	{
		private string directory;

		public IncludeHandler(string directory)
		{
			this.directory = directory;
		}

		public void Close(Stream stream)
		{
			stream.Close();
		}

		public Stream Open(IncludeType type, string fileName, Stream parentStream)
		{
			return File.OpenRead(Path.Combine(directory, fileName));
		}

		public IDisposable Shadow
		{
			get;
			set;
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
