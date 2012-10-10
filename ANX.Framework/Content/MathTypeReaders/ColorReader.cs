#region Using Statements


#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using ANX.Framework.NonXNA.Development;
namespace ANX.Framework.Content
{
    [Developer("GinieDP")]
    internal class ColorReader : ContentTypeReader<Color>
    {
        protected internal override Color Read(ContentReader input, Color existingInstance)
        {
            return input.ReadColor();
        }
    }
}
