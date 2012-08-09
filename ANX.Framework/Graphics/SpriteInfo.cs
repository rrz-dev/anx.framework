#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    internal struct SpriteInfo
    {
        //public Vector2 TopLeft;
        //public Vector2 BottomRight;

        public Vector2[] Corners;

        public Color Tint;
        public Texture2D texture;
        public Single rotation;
        public Vector2 origin;
        public Single layerDepth;

        public Vector2 topLeftUV;
        public Vector2 bottomRightUV;
    }
}
