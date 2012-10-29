#region Using Statements
using System;
using ANX.Framework.NonXNA.Development;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Untested)]
    [Developer("Glatzemann")]
    internal struct SpriteInfo
    {
        public Vector2[] Corners;

        public Color Tint;
        public Texture2D texture;
        public float rotation;
        public Vector2 origin;
		public float layerDepth;

        public Vector2 topLeftUV;
        public Vector2 bottomRightUV;
    }
}
