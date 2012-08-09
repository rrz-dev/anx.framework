#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX.Direct3D10;
using SharpDX.D3DCompiler;
using System.IO;
using ANX.Framework.NonXNA;
using ANX.Framework.Graphics;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
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
            //Console.WriteLine("Including {0} file {1} from directory {2}", type.ToString(), fileName, directory);

            return System.IO.File.OpenRead(System.IO.Path.Combine(directory, fileName));
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
