using ANXStatusComparer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANXStatusComparer.Excludes
{
    public class MethodExclude : Exclude
    {
        public string Name
        {
            get;
            set;
        }

        public AccessModifier Modifier
        {
            get;
            set;
        }

        public override bool ShouldExclude(Data.BaseObjectElement element)
        {
            MethodElement method = element as MethodElement;
            if (method != null && method.Handle.Name == this.Name)
            {
                switch (Modifier)
                {
                    case AccessModifier.Public:
                        return method.Handle.IsPublic;
                    case AccessModifier.Protected:
                        return method.Handle.IsFamily;
                    case AccessModifier.Private:
                        return method.Handle.IsPrivate;
                    case AccessModifier.Internal:
                        return method.Handle.IsAssembly;
                    case AccessModifier.InternalProtected:
                        return method.Handle.IsFamilyOrAssembly;
                }
            }

            return false;
        }
    }
}
