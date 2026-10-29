#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Graphics;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public class AlphaTestMaterialContent : MaterialContent
    {
        public const string AlphaFunctionKey = "";
        public const string AlphaKey = "";
        public const string DiffuseColorKey = "";
        public const string ReferenceAlphaKey = "";
        public const string TextureKey = "";
        public const string VertexColorEnabledKey = "";

        public AlphaTestMaterialContent()
        {
        }

        public Nullable<float> Alpha
        {
            get;
            set;
        }

        public Nullable<CompareFunction> AlphaFunction
        {
            get;
            set;
        }

        public Nullable<Vector3> DiffuseColor
        {
            get;
            set;
        }

        public Nullable<int> ReferenceAlpha
        {
            get;
            set;
        }

        public ExternalReference<TextureContent> Texture
        {
            get;
            set;
        }

        public Nullable<bool> VertexColorEnabled
        {
            get;
            set;
        }
    }
}
