#region Using Statements


#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using ANX.Framework.NonXNA.Development;
namespace ANX.Framework.Content
{
    [Developer("GinieDP")]
    internal class Vector2Reader : ContentTypeReader<Vector2>
    {
        protected internal override Vector2 Read(ContentReader input, Vector2 existingInstance)
        {
            return input.ReadVector2();
        }
    }
}
