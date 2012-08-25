#region Using Statements


#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    internal class MatrixReader : ContentTypeReader<Matrix>
    {
        protected internal override Matrix Read(ContentReader input, Matrix existingInstance)
        {
            return input.ReadMatrix();
        }
    }
}
