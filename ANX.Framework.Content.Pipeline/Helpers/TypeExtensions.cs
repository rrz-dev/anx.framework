using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Helpers
{
    internal static class TypeExtensions
    {
        #region GetGenericMethod
        public static MethodInfo GetGenericMethod(this Type instance, string name, Type[] paramTypes)
        {
            return GetGenericMethod(instance, name, BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance, paramTypes);
        }

        public static MethodInfo GetGenericMethod(this Type instance, string name, BindingFlags bindingAttr, Type[] paramTypes)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (paramTypes == null)
                throw new ArgumentNullException("paramTypes");


            int paramsLength = paramTypes.Length;

            MethodInfo[] methods = instance.GetMethods(bindingAttr);

            IEnumerable<MethodInfo> matchingMethods = methods.Where(x =>
            {
                if (x.IsGenericMethod && x.Name == name)
                {
                    ParameterInfo[] paramInfos = x.GetParameters();
                    if (paramInfos.Length == paramTypes.Length)
                    {
                        for (int i = 0; i < paramTypes.Length; i++)
                        {
                            Type methodParamType = paramInfos[i].ParameterType;
                            Type wantedParamType = paramTypes[i];
                            if (!((methodParamType.FullName != null && methodParamType.AssemblyQualifiedName == wantedParamType.AssemblyQualifiedName) ||
                                methodParamType.FullName == null))
                                return false;
                        }
                        return true;
                    }
                }
                return false;
            });

            int count = matchingMethods.Count();
            if (count > 1)
                throw new AmbiguousMatchException(string.Format("More than one method with the name \"{0}\" on the type {1} found.", name, instance.FullName));
            else if (count == 1)
                return matchingMethods.First();
            else
                return null;
        }
        #endregion

        public static bool IsDefinedBy(this InterfaceMapping instance, Type targetType)
        {
            if (instance.TargetType != targetType)
            {
                return false;
            }

            foreach (MethodInfo method in instance.TargetMethods)
            {
                if (method.DeclaringType != targetType)
                    return false;
            }

            return true;
        }
    }
}
