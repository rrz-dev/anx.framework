using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Helpers
{
    public static class TypeHelper
    {
        static Dictionary<string, Type> types = new Dictionary<string, Type>();

        public static Type GetType(string typeName)
        {
            if (typeName == null)
                throw new ArgumentNullException("typeName");

            Type type = null;
            if (types.TryGetValue(typeName, out type))
            {
                return type;
            }

            //Comma can't be in the name of the type or the namespace, it's only used to specify the assembly of the wanted type.
            bool isAssemblyQualified = typeName.Contains(',');
            //TODO: possible optimization in AssemblyQualified mode to prefilter the assemblies.
            type = Type.GetType(typeName);
            if (type == null)
            {
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    //iterate over the types rather than using Assembly.GetType() because we don't get the not exported types that way.
                    foreach (var t in assembly.GetTypes())
                    {
                        if (isAssemblyQualified)
                        {
                            if (t.AssemblyQualifiedName == typeName)
                            {
                                type = t;
                                break;
                            }
                        }
                        else
                        {
                            if (t.FullName == typeName)
                            {
                                type = t;
                                break;
                            }
                        }
                    }

                    if (type != null)
                        break;
                }

                if (type == null)
                    throw new TypeLoadException(string.Format("The type \"{0}\" can't be found.", typeName));
            }
            types.Add(typeName, type);

            return type;
        }
    }
}
