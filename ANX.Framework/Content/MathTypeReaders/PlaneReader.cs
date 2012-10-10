#region Using Statements


#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using ANX.Framework.NonXNA.Development;
namespace ANX.Framework.Content
{
    [Developer("GinieDP")]
    internal class PlaneReader : ContentTypeReader<Plane>
    {
        protected internal override Plane Read(ContentReader input, Plane existingInstance)
        {
            var result = new Plane();
            result.Normal = input.ReadVector3();
            result.D = input.ReadSingle();
            return result;
        }
    }
}
