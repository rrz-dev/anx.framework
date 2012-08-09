#region Using Statements
using System;
using System.IO;
using ANX.Framework.NonXNA;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input
{
    public struct GamePadThumbSticks
    {
        #region Private Members
        private Vector2 left;
        private Vector2 right;

        #endregion // Private Members

        public GamePadThumbSticks (Vector2 leftThumbstick, Vector2 rightThumbstick)
        {
            this.left = Vector2.Clamp(leftThumbstick, -Vector2.One, Vector2.One);
            this.right = Vector2.Clamp(rightThumbstick, -Vector2.One, Vector2.One);
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == typeof(GamePadThumbSticks))
            {
                return this == (GamePadThumbSticks)obj;
            }

            return false;
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
            return this.left.GetHashCode() ^ this.right.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("{{Left:{0} Right:{1}}}", left, right);
        }

        public Vector2 Left { get { return this.left; } }
        public Vector2 Right { get { return this.right; } }

    }
}
