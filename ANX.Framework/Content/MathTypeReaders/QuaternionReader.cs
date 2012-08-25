#region Using Statements


#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    internal class QuaternionReader : ContentTypeReader<Quaternion>
    {
        protected internal override Quaternion Read(ContentReader input, Quaternion existingInstance)
        {
            var result = new Quaternion();
            result.X = input.ReadSingle();
            result.Y = input.ReadSingle();
            result.Z = input.ReadSingle();
            result.W = input.ReadSingle();
            return result;
        }
    }
}
