#region Using Statements


#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using ANX.Framework.NonXNA.Development;
namespace ANX.Framework.Content
{
    [Developer("GinieDP")]
    public class RayReader : ContentTypeReader<Ray>
    {
        protected internal override Ray Read(ContentReader input, Ray existingInstance)
        {
            var result = new Ray();
            result.Position = input.ReadVector3();
            result.Direction = input.ReadVector3();
            return result;
        }
    }
}
