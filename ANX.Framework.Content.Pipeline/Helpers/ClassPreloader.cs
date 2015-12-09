using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Helpers
{
    public static class ClassPreloader
    {
        /// <summary>
        /// Preloads all classes of the currently loaded assemblies that have the <see cref="PreloadAttribute"/>.
        /// </summary>
        public static void Preload()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Preload(assembly);
            }
        }

        /// <summary>
        /// Preloads all classes of the given <paramref name="assembly"/> that have the <see cref="PreloadAttribute"/>.
        /// </summary>
        /// <param name="assembly"></param>
        public static void Preload(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                //The versions of anx might not always be the same, that's why we're checking for the typename and not the type itself.
                //if (type.GetCustomAttributes(true).Any((x) => x.GetType().Name == "PreloadAttribute"))
                if (type.GetCustomAttributes(typeof(PreloadAttribute), true).Any())
                {
                    System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                }
            }
        }
    }
}
