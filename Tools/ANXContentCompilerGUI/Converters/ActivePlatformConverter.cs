using ANX.ContentCompiler.GUI.Nodes;
using ANX.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ANX.ContentCompiler.GUI.Converters
{
    class ActivePlatformConverter : EnumConverter
    {
        public ActivePlatformConverter()
            : base(typeof(TargetPlatform))
        {

        }
    }
}
