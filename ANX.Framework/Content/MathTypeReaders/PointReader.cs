#region Using Statements


#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using ANX.Framework.NonXNA.Development;
namespace ANX.Framework.Content
{
    [Developer("GinieDP")]
    internal class PointReader : ContentTypeReader<Point>
    {
        protected internal override Point Read(ContentReader input, Point existingInstance)
        {
            var result = new Point();
            result.X = input.ReadInt32();
            result.Y = input.ReadInt32();
            return result;
        }
    }
}
