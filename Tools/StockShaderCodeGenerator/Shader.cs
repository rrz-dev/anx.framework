#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace StockShaderCodeGenerator
{
    public struct Shader
    {
        public string Type;
        public string Source;
        public string RenderSystem;
        public bool ShaderCompiled;
        public byte[] ByteCode;
    }
}
