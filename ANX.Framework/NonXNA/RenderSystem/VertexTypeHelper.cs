using System;
using System.Collections.Generic;
using ANX.Framework.Graphics;

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

			return rememberedInstances[type].VertexDeclaration;
		}
	}
}
