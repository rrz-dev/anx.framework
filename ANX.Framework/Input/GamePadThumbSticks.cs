using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.Untested)]
    public struct GamePadThumbSticks
    {
        #region Private
        private Vector2 left;
        private Vector2 right;
		#endregion

		public Vector2 Left
		{
			get { return this.left; }
		}

		public Vector2 Right
		{
			get { return this.right; }
		}

        public GamePadThumbSticks (Vector2 leftThumbstick, Vector2 rightThumbstick)
        {
            left = Vector2.Clamp(leftThumbstick, -Vector2.One, Vector2.One);
            right = Vector2.Clamp(rightThumbstick, -Vector2.One, Vector2.One);
        }

        public override bool Equals(object obj)
        {
            return obj is GamePadThumbSticks && this == (GamePadThumbSticks)obj;
        }

	    public static bool operator ==(GamePadThumbSticks lhs, GamePadThumbSticks rhs)
        {
            return lhs.left == rhs.left && lhs.right == rhs.right;
        }

        public static bool operator !=(GamePadThumbSticks lhs, GamePadThumbSticks rhs)
        {
            return lhs.left != rhs.left || lhs.right != rhs.right;
        }

        public override int GetHashCode()
        {
            return left.GetHashCode() ^ right.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("{{Left:{0} Right:{1}}}", left, right);
        }

    }
}
