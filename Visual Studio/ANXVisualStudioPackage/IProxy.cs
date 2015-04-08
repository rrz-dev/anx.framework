using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio
{
    public interface IProxy
    {
        object OriginalInstance
        {
            get;
        }

        Type WrapperType
        {
            get;
        }
    }
}
