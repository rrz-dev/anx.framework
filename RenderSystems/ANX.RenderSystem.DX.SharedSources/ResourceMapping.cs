using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if DX10
namespace ANX.RenderSystem.Windows.DX10
#elif DX11
namespace ANX.RenderSystem.Windows.DX11
#endif
{
    public enum ResourceMapping
    {
        Read = 1,
        Write = 2,
        ReadWrite = 3,
    }
}
