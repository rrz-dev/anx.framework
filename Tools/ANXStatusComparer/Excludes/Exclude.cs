using ANXStatusComparer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANXStatusComparer.Excludes
{
    public abstract class Exclude
    {
        public abstract bool ShouldExclude(BaseObjectElement element);
    }
}
