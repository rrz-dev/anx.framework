#region Using Statements
using System;
using System.Reflection;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler
{
    public abstract class ContentTypeWriter
    {
        private bool canDeserializeIntoExistingObject;
        private Type targetType;
        internal readonly bool TargetIsValueType;

        protected ContentTypeWriter(Type targetType)
        {
            this.targetType = targetType;
        }

        internal static string GetStrongTypeName(Type type, TargetPlatform targetPlatform)
        {
            string text = ContentTypeWriter.GetTypeName(type);
            if (!string.IsNullOrEmpty(type.Namespace))
            {
                text = type.Namespace + '.' + text;
            }
            return text + ", " + ContentTypeWriter.GetAssemblyFullName(type.Assembly, targetPlatform);
        }

        internal static string GetAssemblyFullName(Assembly assembly, TargetPlatform targetPlatform)
        {
            AssemblyName assemblyName = assembly.GetName();
            //ContentTypeWriter.NetCFPlatformDescription[] netCFDescs = ContentTypeWriter.NetCFDescs;
            //for (int i = 0; i < netCFDescs.Length; i++)
            //{
            //    ContentTypeWriter.NetCFPlatformDescription netCFPlatformDescription = netCFDescs[i];
            //    if (netCFPlatformDescription.TargetPlatform == targetPlatform)
            //    {
            //        assemblyName = (AssemblyName)assemblyName.Clone();
            //        if (ContentTypeWriter.KeysAreEqual(assemblyName.GetPublicKeyToken(), ContentTypeWriter.WindowsPublicKeyToken))
            //        {
            //            assemblyName.SetPublicKeyToken(netCFPlatformDescription.PublicKeyToken);
            //            break;
            //        }
            //        string[] netCFAssemblies = netCFPlatformDescription.NetCFAssemblies;
            //        for (int j = 0; j < netCFAssemblies.Length; j++)
            //        {
            //            string value = netCFAssemblies[j];
            //            if (assemblyName.Name.Equals(value, StringComparison.InvariantCulture))
            //            {
            //                assemblyName.Version = netCFPlatformDescription.NetCFAssemblyVersion;
            //                assemblyName.SetPublicKeyToken(netCFPlatformDescription.NetCFPublicKeyToken);
            //                break;
            //            }
            //        }
            //    }
            //}
            return assemblyName.FullName;
        }

        private static string GetTypeName(Type type)
        {
            string text = type.Name;
            Type declaringType = type.DeclaringType;
            if (declaringType != null)
            {
                text = ContentTypeWriter.GetTypeName(declaringType) + '+' + text;
            }
            return text;
        }

        internal string GetGenericArgumentRuntimeTypes(TargetPlatform targetPlatform)
        {
            //TODO: implement
            System.Diagnostics.Debugger.Break();
            return "";

            //if (this.genericArgumentWriters == null)
            //{
            //    return string.Empty;
            //}
            //string text = string.Empty;
            //for (int i = 0; i < this.genericArgumentWriters.Count; i++)
            //{
            //    if (i > 0)
            //    {
            //        text += ',';
            //    }
            //    object obj = text;
            //    text = string.Concat(new object[]
            //    {
            //        obj,
            //        '[',
            //        this.genericArgumentWriters[i].GetRuntimeType(targetPlatform),
            //        ']'
            //    });
            //}
            //return '[' + text + ']';
        }

        public abstract string GetRuntimeReader(TargetPlatform targetPlatform);

        public virtual string GetRuntimeType(TargetPlatform targetPlatform)
        {
            throw new NotImplementedException();
        }

        protected virtual void Initialize(ContentCompiler compiler)
        {
            throw new NotImplementedException();
        }

        protected internal virtual bool ShouldCompressContent(TargetPlatform targetPlatform, Object value)
        {
            throw new NotImplementedException();
        }

        protected internal abstract void Write(ContentWriter output, Object value);

        public virtual bool CanDeserializeIntoExistingObject 
        {
            get
            {
                return canDeserializeIntoExistingObject;
            }
        }

        public Type TargetType
        {
            get
            {
                return this.targetType;
            }
        }

        public virtual int TypeVersion
        {
            get
            {
                return 0;
            }
        }
    }
}
