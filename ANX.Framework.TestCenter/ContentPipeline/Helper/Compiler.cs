#region Using Statements
using System;
using System.IO;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using XnaFrame = Microsoft.Xna.Framework;
using AnxFrame = ANX.Framework;

namespace ANX.Framework.TestCenter.ContentPipeline.Helper
{
    public class Compiler
    {
        private ContentCompiler compiler;
        private string contentRootDirectory;
        private string referenceLocationPath;

        public Compiler()
        {
            Type type = typeof(ContentCompiler);
            ConstructorInfo ci = type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);
            this.compiler = ci.Invoke(null) as ContentCompiler;

            string directory = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;
            contentRootDirectory = Path.Combine(directory, "Content");
            referenceLocationPath = Path.Combine(directory, "Reference");
        }

        private MethodInfo GetPrivateMethod(string name)
        {
            return compiler.GetType().GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public void WriteAsset(
            Stream output, object value,
            Microsoft.Xna.Framework.Content.Pipeline.TargetPlatform targetPlatform,
            Microsoft.Xna.Framework.Graphics.GraphicsProfile targetProfile,
            bool compressContent,
            string rootDirectory,
            string referenceLocationPath)
        {
            MethodInfo compile = GetPrivateMethod("Compile");
            compile.Invoke(compiler, new object[] { 
                output, value, targetPlatform, targetProfile, compressContent, rootDirectory, referenceLocationPath
            });
        }

        public void WriteAsset(Stream output, object value)
        {
            WriteAsset(output,
                value,
                Microsoft.Xna.Framework.Content.Pipeline.TargetPlatform.Windows,
                Microsoft.Xna.Framework.Graphics.GraphicsProfile.HiDef, 
                false, contentRootDirectory, referenceLocationPath);
        }

        public void WriteAsset(Stream output, object value, bool compress)
        {
            WriteAsset(output,
                value,
                Microsoft.Xna.Framework.Content.Pipeline.TargetPlatform.Windows,
                Microsoft.Xna.Framework.Graphics.GraphicsProfile.HiDef,
                compress, contentRootDirectory, referenceLocationPath);
        }

        public void AddTypeWriter(ContentTypeWriter writer)
        {
            MethodInfo addTypeWriter = GetPrivateMethod("AddTypeWriter");
            addTypeWriter.Invoke(compiler, new object[] { writer });
        }
    }
}
