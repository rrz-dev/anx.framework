using System;

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler
{
	internal abstract class BuiltinTypeWriter<T> : ContentTypeWriter<T>
	{
		public override string GetRuntimeReader(TargetPlatform targetPlatform)
		{
			// TODO!
			return "";
		}
	}
}
