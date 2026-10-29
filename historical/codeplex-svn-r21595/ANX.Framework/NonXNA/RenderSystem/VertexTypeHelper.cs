using System;
using System.Collections.Generic;
using ANX.Framework.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.RenderSystem
{
	internal static class VertexTypeHelper
	{
		private static Dictionary<Type, IVertexType> rememberedInstances = new Dictionary<Type, IVertexType>();

		public static VertexDeclaration GetDeclaration<T>() where T : struct, IVertexType
		{
			Type type = typeof(T);
			if (rememberedInstances.ContainsKey(type) == false)
				rememberedInstances.Add(type, Activator.CreateInstance<T>());

			var result = rememberedInstances[type];
			return result != null ? result.VertexDeclaration : null;
		}

		public static VertexDeclaration GetDeclaration(Type type)
		{
			if (rememberedInstances.ContainsKey(type) == false)
				rememberedInstances.Add(type, Activator.CreateInstance(type) as IVertexType);

			var result = rememberedInstances[type];
			return result != null ? result.VertexDeclaration : null;
		}
	}
}
