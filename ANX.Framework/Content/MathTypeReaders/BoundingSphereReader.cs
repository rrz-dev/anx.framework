#region Using Statements


#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using ANX.Framework.NonXNA.Development;
namespace ANX.Framework.Content
{
    [Developer("GinieDP")]
    internal class BoundingSphereReader : ContentTypeReader<BoundingSphere>
    {
        protected internal override BoundingSphere Read(ContentReader input, BoundingSphere existingInstance)
        {
            var result = new BoundingSphere();
            result.Center = input.ReadVector3();
            result.Radius = input.ReadSingle();
            return result;
        }
    }
}
