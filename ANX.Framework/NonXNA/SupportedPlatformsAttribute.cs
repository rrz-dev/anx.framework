using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.NonXNA
{
    [CLSCompliant(false)]
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    public sealed class SupportedPlatformsAttribute : Attribute
    {
        public SupportedPlatformsAttribute(params PlatformName[] platforms)
        {
            this.Platforms = platforms;
        }

        public PlatformName[] Platforms
        {
            get;
            set;
        }
    }
}
