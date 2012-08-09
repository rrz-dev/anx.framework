#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    internal class CharReader : ContentTypeReader<Char>
    {
        protected internal override Char Read(ContentReader input, Char existingInstance)
        {
            return input.ReadChar();
        }
    }
}
