#region Using Statements
using System;
using ANX.Framework;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.GamerServices
{
    public struct AvatarExpression
    {
        public AvatarEye LeftEye { get; set; }

        public AvatarEyebrow LeftEyebrow { get; set; }

        public AvatarMouth Mouth { get; set; }

        public AvatarEye RightEye { get; set; }

        public AvatarEyebrow RightEyebrow { get; set; }
    }
}
