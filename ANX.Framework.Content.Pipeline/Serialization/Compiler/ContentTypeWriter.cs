#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler
{
    public abstract class ContentTypeWriter
    {
        private List<ContentTypeWriter> _genericArgumentWriters;
        internal readonly bool TargetIsValueType;

        #region CFPlatformDesc
        private static readonly NetCfPlatformDescription[] NetCfDescs = new[]
		{
			new NetCfPlatformDescription(TargetPlatform.XBox360, new byte[]
			{
				132,
				44,
				248,
				190,
				29,
				229,
				5,
				83
			}, new string[]
			{
				"mscorlib",
				"System",
				"System.Xml"
			}, new byte[]
			{
				28,
				158,
				37,
				150,
				134,
				249,
				33,
				224
			}, new Version(3, 7, 0, 0)),
			new NetCfPlatformDescription(TargetPlatform.WindowsPhone, new byte[]
			{
				132,
				44,
				248,
				190,
				29,
				229,
				5,
				83
			}, new string[]
			{
				"mscorlib",
				"System",
				"System.Xml"
			}, new byte[]
			{
				150,
				157,
				184,
				5,
				61,
				51,
				34,
				172
			}, new Version(3, 7, 0, 0))
		};
        #endregion

        #region PublicKeyToken
        private static readonly byte[] WindowsPublicKeyToken = new byte[]
		{
			132,
			44,
			248,
			190,
			29,
			229,
			5,
			83
		};
        #endregion

        protected ContentTypeWriter(Type targetType)
        {
            TargetType = targetType;
            TargetIsValueType = targetType.IsValueType;
        }

        internal static string GetStrongTypeName(Type type, TargetPlatform targetPlatform)
        {
            string text = GetTypeName(type);
            if (!string.IsNullOrEmpty(type.Namespace))
            {
                text = type.Namespace + '.' + text;
            }
            return text + ", " + GetAssemblyFullName(type.Assembly, targetPlatform);
        }

        internal static string GetAssemblyFullName(Assembly assembly, TargetPlatform targetPlatform)
        {
            AssemblyName assemblyName = assembly.GetName();
            NetCfPlatformDescription[] netCfDescs = NetCfDescs;
            foreach (var netCfPlatformDescription in netCfDescs.Where(netCfPlatformDescription => netCfPlatformDescription.TargetPlatform == targetPlatform))
            {
                assemblyName = (AssemblyName)assemblyName.Clone();
                if (ContentTypeWriter.KeysAreEqual(assemblyName.GetPublicKeyToken(), WindowsPublicKeyToken))
                {
                    assemblyName.SetPublicKeyToken(netCfPlatformDescription.PublicKeyToken);
                    break;
                }
                var netCfAssemblies = netCfPlatformDescription.NetCfAssemblies;
                if (netCfAssemblies.Any(value => assemblyName.Name.Equals(value, StringComparison.InvariantCulture)))
                {
                    assemblyName.Version = netCfPlatformDescription.NetCfAssemblyVersion;
                    assemblyName.SetPublicKeyToken(netCfPlatformDescription.NetCfPublicKeyToken);
                }
            }
            return assemblyName.FullName;
        }

        private static string GetTypeName(Type type)
        {
            string text = type.Name;
            Type declaringType = type.DeclaringType;
            if (declaringType != null)
            {
                text = GetTypeName(declaringType) + '+' + text;
            }
            return text;
        }

        private static bool KeysAreEqual(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }
            return true;
        }

        internal string GetGenericArgumentRuntimeTypes(TargetPlatform targetPlatform)
        {
            if (_genericArgumentWriters == null)
            {
                return string.Empty;
            }
            var text = string.Empty;
            for (var i = 0; i < _genericArgumentWriters.Count; i++)
            {
                if (i > 0)
                {
                    text += ',';
                }
                object obj = text;
                text = string.Concat(new[]
                {
                    obj,
                    '[',
                    _genericArgumentWriters[i].GetRuntimeType(targetPlatform),
                    ']'
                });
            }
            return '[' + text + ']';
        }

        internal void DoInitialize(ContentCompiler compiler)
        {
            Initialize(compiler);
            if (TargetType.IsGenericType)
            {
                _genericArgumentWriters = new List<ContentTypeWriter>();
                Type[] genericArguments = TargetType.GetGenericArguments();
                foreach (var type in genericArguments)
                {
                    _genericArgumentWriters.Add(compiler.GetTypeWriter(type));
                }
            }
        }

        public abstract string GetRuntimeReader(TargetPlatform targetPlatform);

        public virtual string GetRuntimeType(TargetPlatform targetPlatform)
        {
            var text = GetTypeName(TargetType);
            if (!string.IsNullOrEmpty(TargetType.Namespace))
            {
                text = TargetType.Namespace + '.' + text;
            }
            text += GetGenericArgumentRuntimeTypes(targetPlatform);
            return text + ", " + GetAssemblyFullName(TargetType.Assembly, targetPlatform);
        }

        protected virtual void Initialize(ContentCompiler compiler)
        {
            
        }

        protected internal virtual bool ShouldCompressContent(TargetPlatform targetPlatform, Object value)
        {
            throw new NotImplementedException();
        }

        protected internal abstract void Write(ContentWriter output, Object value);

        public virtual bool CanDeserializeIntoExistingObject
        {
            get; 
            set;
        }

        public Type TargetType
        {
            get; 
            set; 
        }

        public virtual int TypeVersion
        {
            get
            {
                return 0;
            }
        }

        internal class NetCfPlatformDescription
        {
            public TargetPlatform TargetPlatform;
            public byte[] PublicKeyToken;
            public string[] NetCfAssemblies;
            public byte[] NetCfPublicKeyToken;
            public Version NetCfAssemblyVersion;
            public NetCfPlatformDescription(TargetPlatform targetPlatform, byte[] publicKeyToken, string[] netCfAssemblies, byte[] netCfPublicKeyToken, Version netCfAssemblyVersion)
            {
                TargetPlatform = targetPlatform;
                PublicKeyToken = publicKeyToken;
                NetCfAssemblies = netCfAssemblies;
                NetCfPublicKeyToken = netCfPublicKeyToken;
                NetCfAssemblyVersion = netCfAssemblyVersion;
            }
        }
    }
}
