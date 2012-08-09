using System;
using System.Collections.Generic;
using System.Reflection;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
	internal static class TypeHelper
	{
		#region GetInterfacesFrom
		public static Type[] GetInterfacesFrom(Type type)
		{
#if WINDOWSMETRO
            var interfaces = type.GetTypeInfo().ImplementedInterfaces;
            var enumerator = interfaces.GetEnumerator();
            List<Type> types = new List<Type>();
            while (enumerator.MoveNext())
            {
                types.Add(enumerator.Current);
            }
            return types.ToArray();
#else
			return type.GetInterfaces();
#endif
		}
		#endregion

		#region IsTypeAssignableFrom
		public static bool IsTypeAssignableFrom(Type baseType, Type typeToCheck)
		{
#if WINDOWSMETRO
			return baseType.GetTypeInfo().IsAssignableFrom(typeToCheck.GetTypeInfo());
#else
			return baseType.IsAssignableFrom(typeToCheck);
#endif
		}
		#endregion

		#region IsAnyTypeAssignableFrom
		public static bool IsAnyTypeAssignableFrom(Type[] baseTypes, Type typeToCheck)
		{
			foreach (Type baseType in baseTypes)
			{
				if (IsTypeAssignableFrom(baseType, typeToCheck))
				{
					return true;
				}
			}

			return false;
		}
		#endregion

		#region SafelyExtractTypesFrom
		public static Type[] SafelyExtractTypesFrom(Assembly assembly)
		{
			Type[] result = new Type[0];
			try
			{
#if WINDOWSMETRO
                var enumerator = assembly.DefinedTypes.GetEnumerator();
                List<Type> allTypes = new List<Type>();
                while (enumerator.MoveNext())
                {
                    allTypes.Add(enumerator.Current.AsType());
                }

                return allTypes.ToArray();
#else
				result = assembly.GetTypes();
#endif
			}
			catch (ReflectionTypeLoadException ex)
			{
				result = ex.Types;
			}

			return result;
		}
		#endregion

		#region IsValueType
		public static bool IsValueType(Type type)
		{
#if WINDOWSMETRO
			return type.GetTypeInfo().IsValueType;
#else
			return type.IsValueType;
#endif
		}
		#endregion

		#region IsAbstract
		public static bool IsAbstract(Type type)
		{
#if WINDOWSMETRO
			return type.GetTypeInfo().IsAbstract;
#else
			return type.IsAbstract;
#endif
		}
		#endregion
	}
}
