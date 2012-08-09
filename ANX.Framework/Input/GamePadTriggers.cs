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
    public struct GamePadTriggers
    {
        #region Private Members
        private float left;
        private float right;

        #endregion // Private Members

        public GamePadTriggers (float leftTrigger,float rightTrigger)
        {
            if (leftTrigger>1)
            {
                leftTrigger = 1;
            }
            if (leftTrigger<0)
            {
                leftTrigger = 0;
            }
            if (rightTrigger>1)
            {
                rightTrigger = 1;
            }
            if (rightTrigger<0)
            {
                rightTrigger = 0;
            }
            left =  leftTrigger;
            right = rightTrigger;
        }

        public override int GetHashCode()
        {
            return left.GetHashCode() ^ right.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("{{Left:{0} Right:{1}}}", left, right);
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == typeof(GamePadTriggers))
            {
                return this == (GamePadTriggers)obj;
            }

            return false;
        }

        public static bool operator ==(GamePadTriggers lhs, GamePadTriggers rhs)
        {
            return lhs.left == rhs.left && lhs.right == rhs.right;
        }

        public static bool operator !=(GamePadTriggers lhs, GamePadTriggers rhs)
        {
            return lhs.left != rhs.left || lhs.right != rhs.right;
        }

        public float Left { get { return this.left; } }
        public float Right { get { return this.right; } }

    }
}
