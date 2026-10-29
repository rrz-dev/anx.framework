#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Graphics;
using System.ComponentModel;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Processors
{
    public class ModelProcessor : ContentProcessor<NodeContent, ModelContent>
    {
        public virtual Color ColorKeyColor { get; set; }
        public virtual bool ColorKeyEnabled { get; set; }
        public virtual MaterialProcessorDefaultEffect DefaultEffect { get; set; }
        public virtual bool GenerateMipmaps { get; set; }
        public virtual bool GenerateTangentFrames { get; set; }
        [DefaultValue(true)]
        public virtual bool PremultiplyTextureAlpha { get; set; }
        [DefaultValue(true)]
        public virtual bool PremultiplyVertexColors { get; set; }
        public virtual bool ResizeTexturesToPowerOfTwo { get; set; }
        public virtual float RotationX { get; set; }
        public virtual float RotationY { get; set; }
        public virtual float RotationZ { get; set; }
        public virtual float Scale { get; set; }
        public virtual bool SwapWindingOrder { get; set; }
        public virtual TextureProcessorOutputFormat TextureFormat { get; set; }

        public override ModelContent Process(NodeContent input, ContentProcessorContext context)
        {
            throw new NotImplementedException();
        }

        protected virtual MaterialContent ConvertMaterial(MaterialContent material, ContentProcessorContext context)
        {
            throw new NotImplementedException();
        }

        protected virtual void ProcessGeometryUsingMaterial(MaterialContent material, IEnumerable<GeometryContent> geometryContent, ContentProcessorContext context)
        {
            throw new NotImplementedException();
        }

        protected virtual void ProcessVertexChannel(GeometryContent geometry, int vertexChannelIndex, ContentProcessorContext context)
        {
            throw new NotImplementedException();
        }
    }
}
