#region Using Statements
using System;
using System.IO;
using ANX.Framework.Graphics;
using System.Collections.Generic;
using System.Reflection;
using ANX.Framework.Content.Pipeline.Helpers;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler
{
    public sealed class ContentCompiler
    {
        #region Private Members
        private Dictionary<Type, ContentTypeWriter> writerInstances = new Dictionary<Type, ContentTypeWriter>();
        private Dictionary<Type, Type> genericHandlers = new Dictionary<Type, Type>();

        #endregion

        public ContentCompiler()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!AssemblyHelper.IsValidForPipeline(assembly.GetName()))
                    continue;

                AddContentWriterAssembly(assembly);
            }
        }

        public ContentTypeWriter GetTypeWriter(Type type)
        {
            IEnumerable<Type> dependencies;
            return GetTypeWriter(type, out dependencies);
        }

        public ContentTypeWriter GetTypeWriter(Type type, out IEnumerable<Type> dependencies)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            dependencies = new List<Type>();

            ContentTypeWriter contentTypeWriter;
            if (!this.writerInstances.TryGetValue(type, out contentTypeWriter))
            {
                if (type.IsGenericType)
                {
                    Type genericTypeDefinition = type.GetGenericTypeDefinition();
                    Type handler;
                    if (this.genericHandlers.TryGetValue(genericTypeDefinition, out handler))
                    {
                        Type genericType = handler.MakeGenericType(type.GetGenericArguments());
                        contentTypeWriter = ((object)Activator.CreateInstance(genericType)) as ContentTypeWriter;

                        foreach (Type dependentType in type.GetGenericArguments())
                        {
                            if (!((List<Type>)dependencies).Contains(dependentType))
                            {
                                ((List<Type>)dependencies).Add(dependentType);
                            }
                        }
                    }
                }
                else
                {
                    contentTypeWriter = default(ContentTypeWriter);
                }

                if (contentTypeWriter == null)
                {
                    if (type.IsArray)
                    {
                        if (type.GetArrayRank() != 1)
                        {
                            throw new RankException("can't serialize multidimensional arrays");
                        }

                        contentTypeWriter = Activator.CreateInstance(typeof(ArrayWriter<>).MakeGenericType(new Type[] { type.GetElementType() })) as ContentTypeWriter;
                    }

                    if (type.IsEnum)
                    {
                        contentTypeWriter = Activator.CreateInstance(typeof(EnumWriter<>).MakeGenericType(new Type[] { type.GetElementType() })) as ContentTypeWriter;
                    }

                    //TODO: return new ReflectiveWriter(type);
                }

                if (contentTypeWriter != null)
                {
                    this.writerInstances.Add(type, contentTypeWriter);
                    contentTypeWriter.DoInitialize(this);
                }
            }

            return contentTypeWriter;
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

            using (ContentWriter contentWriter = new ContentWriter(this, output, compressContent, rootDirectory, referenceRelocationPath) { TargetPlatform = targetPlatform, TargetProfile = targetProfile })
            {
                contentWriter.WriteObject<object>(value);
            }
        }

        private bool ShouldCompressContent(TargetPlatform targetPlatform, object value)
        {
            if (targetPlatform == TargetPlatform.WindowsPhone)
            {
                return false;
            }

            ContentTypeWriter typeWriterInternal = this.GetTypeWriter(value.GetType());
            
            return typeWriterInternal.ShouldCompressContent(targetPlatform, value);
        }

        public void AddContentWriterAssembly(Assembly assembly)
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
                        Type baseType = type.BaseType;
                        while (!baseType.IsGenericType || baseType.GetGenericTypeDefinition() != typeof(ContentTypeWriter<>))
                        {
                            baseType = baseType.BaseType;
                        }

                        Type genericType = baseType.GetGenericArguments()[0];
                        Type genericTypeDefinition = genericType.GetGenericTypeDefinition();
                        genericHandlers.Add(genericTypeDefinition, type);
                    }
                }
            }
        }
    }
}
