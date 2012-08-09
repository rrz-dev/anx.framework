#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using ANX.Framework.Graphics;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    public class AlphaTestEffectReader : ContentTypeReader<AlphaTestEffect>
    {
        protected internal override AlphaTestEffect Read(ContentReader input, AlphaTestEffect existingInstance)
        {
            var graphics = input.ResolveGraphicsDevice();
            var effect = new AlphaTestEffect(graphics);
            
            effect.Texture = input.ReadExternalReference<Texture2D>();
            effect.AlphaFunction = (CompareFunction)input.ReadInt32();
            effect.ReferenceAlpha = input.ReadInt32();
            effect.DiffuseColor = input.ReadVector3();
            effect.Alpha = input.ReadSingle();
            effect.VertexColorEnabled = input.ReadBoolean();
            return effect;
        }
    }
}