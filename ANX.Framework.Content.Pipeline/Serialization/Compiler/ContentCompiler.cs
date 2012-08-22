#region Using Statements
using System;
using System.IO;
using ANX.Framework.Graphics;
using System.Collections.Generic;
using System.Reflection;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler
{
    public sealed class ContentCompiler
    {
        private Dictionary<Type, ContentTypeWriter> writerInstances = new Dictionary<Type, ContentTypeWriter>();

        public ContentCompiler()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    ContentTypeWriterAttribute[] value = (ContentTypeWriterAttribute[])type.GetCustomAttributes(typeof(ContentTypeWriterAttribute), true);
                    if (value.Length > 0)
                    {
                        if (!type.ContainsGenericParameters)
                        {
                            ContentTypeWriter writer = (ContentTypeWriter)Activator.CreateInstance(type);
                            writerInstances[writer.TargetType] = writer;
                        }
                        else
                        {
                            //TODO: implement generic writer instances...
                            System.Diagnostics.Debugger.Break();
                        }
                    }
                }
            }
        }

        internal void Compile(Stream output, object value, TargetPlatform targetPlatform, GraphicsProfile targetProfile, bool compressContent, string rootDirectory, string referenceRelocationPath)
        {
            if (output == null)
            {
                throw new ArgumentNullException("output");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (!Enum.IsDefined(typeof(TargetPlatform), targetPlatform))
            {
                throw new ArgumentOutOfRangeException("targetPlatform");
            }
            if (!Enum.IsDefined(typeof(GraphicsProfile), targetProfile))
            {
                throw new ArgumentOutOfRangeException("targetProfile");
            }
            if (string.IsNullOrEmpty(rootDirectory))
            {
                throw new ArgumentNullException("rootDirectory");
            }
            if (string.IsNullOrEmpty(referenceRelocationPath))
            {
                throw new ArgumentNullException("referenceRelocationPath");
            }
            if (compressContent)
            {
                compressContent = this.ShouldCompressContent(targetPlatform, value);
            }
            using (ContentWriter contentWriter = new ContentWriter(this, output, targetPlatform, targetProfile, compressContent, rootDirectory, referenceRelocationPath))
            {
                contentWriter.WriteObject<object>(value);
                contentWriter.FlushOutput();
            }
        }

        public ContentTypeWriter GetTypeWriter(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            ContentTypeWriter typeWriterInternal = this.GetTypeWriterInternal(type);
            //TODO: this.RecordDependency(typeWriterInternal.TargetType);
            return typeWriterInternal;
        }

        private ContentTypeWriter GetTypeWriterInternal(Type type)
        {
            ContentTypeWriter contentTypeWriter;
            if (!this.writerInstances.TryGetValue(type, out contentTypeWriter))
            {
                //contentTypeWriter = this.typeWriterFactory.CreateWriter(type);
                //this.AddTypeWriter(contentTypeWriter);
                //this.InitializeTypeWriter(contentTypeWriter);
            }
            return contentTypeWriter;
        }

        private bool ShouldCompressContent(TargetPlatform targetPlatform, object value)
        {
            if (targetPlatform == TargetPlatform.WindowsPhone)
            {
                return false;
            }
            ContentTypeWriter typeWriterInternal = this.GetTypeWriterInternal(value.GetType());
            return typeWriterInternal.ShouldCompressContent(targetPlatform, value);
        }
    }
}
