#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ProjectConverter.Platforms
{
    public class XnaConverter : AbstractXna2AnxConverter
    {
        public override string Name
        {
            get { return "xna2anx"; }
        }

        protected internal override MappingDirection MappingDirection
        {
            get { return ProjectConverter.MappingDirection.Xna2Anx; }
        }

        protected internal override string ReplaceInlineNamespaces(string input)
        {
            return input.Replace("Microsoft.Xna.Framework.Game", "ANX.Framework.Game");
        }

    }
}
