using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.NonXNA
{
    //Used to attach custom editors to types which don't know of the editor.
    //The build tools like the content compiler and the extension for visual studio use this.
    /// <summary>
    /// States that the type should be loaded as early as possible.
    /// </summary>
    [Developer("KorsarNek")]
    [AttributeUsage(AttributeTargets.Class)]
    public class PreloadAttribute : Attribute
    {
    }
}
