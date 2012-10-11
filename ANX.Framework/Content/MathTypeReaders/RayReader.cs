#region Using Statements
using ANX.Framework.NonXNA.Development;
#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    [PercentageComplete(100)]
    [Developer("GinieDP")]
    [TestState(TestStateAttribute.TestState.Untested)]
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
