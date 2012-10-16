using System;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input
{
	[PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Tested)]
	public struct GamePadTriggers
    {
        private readonly float left;
        private readonly float right;

        public float Left
        {
            get { return left; }
        }

        public float Right
        {
            get { return right; }
        }

		public GamePadTriggers(float leftTrigger, float rightTrigger)
		{
            left = MathHelper.Clamp(leftTrigger, 0f, 1f);
            right = MathHelper.Clamp(rightTrigger, 0f, 1f);
		}

		public override int GetHashCode()
        {
            return HashHelper.GetGCHandleHashCode(this);
		}

		public override string ToString()
		{
            return String.Format("{{Left:{0} Right:{1}}}", left, right);
		}

		public override bool Equals(object obj)
		{
		    return obj is GamePadTriggers && this == (GamePadTriggers)obj;
		}

	    public static bool operator ==(GamePadTriggers lhs, GamePadTriggers rhs)
		{
            return lhs.left == rhs.left && lhs.right == rhs.right;
		}

		public static bool operator !=(GamePadTriggers lhs, GamePadTriggers rhs)
		{
            return lhs.left != rhs.left || lhs.right != rhs.right;
		}
	}
}
