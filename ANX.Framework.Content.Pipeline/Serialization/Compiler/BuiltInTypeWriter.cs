﻿#region Using Statements
using System;

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
			// TODO!
			return "";
		}
	}
}
