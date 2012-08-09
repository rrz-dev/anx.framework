#region Using Statements
using System;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    internal class RectangleReader : ContentTypeReader<Rectangle>
    {
        protected internal override Rectangle Read(ContentReader input, Rectangle existingInstance)
        {
            var result = new Rectangle();
            result.X = input.ReadInt32();
            result.Y = input.ReadInt32();
            result.Width = input.ReadInt32();
            result.Height = input.ReadInt32();
            return result;
        }
    }
}
