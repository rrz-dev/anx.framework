using System;
using System.Collections.Generic;
using StringPair = System.Collections.Generic.KeyValuePair<string, string>;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.GL3
{
	public class ShaderData
	{
		public string VertexGlobalCode;

		public Dictionary<string, string> VertexShaderCodes;

		public string FragmentGlobalCode;

		public Dictionary<string, string> FragmentShaderCodes;

		public Dictionary<string, StringPair> Techniques;

		public ShaderData()
		{
			VertexShaderCodes = new Dictionary<string, string>();
			FragmentShaderCodes = new Dictionary<string, string>();
			Techniques = new Dictionary<string, StringPair>();
		}
	}
}
