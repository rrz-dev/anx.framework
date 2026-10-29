#region Using Statements
using System;
using System.Reflection;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler
{
	internal abstract class BuiltinTypeWriter<T> : ContentTypeWriter<T>
	{
		public override string GetRuntimeReader(TargetPlatform targetPlatform)
		{
            string @namespace = typeof(ContentTypeReader).Namespace;
            string text = base.GetType().Name.Replace("Writer", "Reader");
            text += base.GetGenericArgumentRuntimeTypes(targetPlatform);
            Assembly runtimeAssembly = this.RuntimeAssembly;
            if (runtimeAssembly != null)
            {
                text = text + ", " + ContentTypeWriter.GetAssemblyFullName(runtimeAssembly, targetPlatform);
            }
            return @namespace + '.' + text;
        }

        protected virtual Assembly RuntimeAssembly
        {
            get
            {
                return null;
            }
        }
	}
}
