﻿#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public class TextureCubeContent : TextureContent
    {
        public TextureCubeContent()
            : base(null)    //TODO: implement
        {

        }

        public override void Validate(Framework.Graphics.GraphicsProfile? targetProfile)
        {
            throw new NotImplementedException();
        }
    }
}
