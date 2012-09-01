using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.Untested)]
	public struct GamePadTriggers
	{
		public float Left { get; private set; }
		public float Right { get; private set; }

		public GamePadTriggers(float leftTrigger, float rightTrigger)
			: this()
		{
			Left = MathHelper.Clamp(leftTrigger, 0f, 1f);
			Right = MathHelper.Clamp(rightTrigger, 0f, 1f);
		}

		public override int GetHashCode()
		{
			return Left.GetHashCode() ^ Right.GetHashCode();
		}

		public override string ToString()
		{
			return String.Format("{{Left:{0} Right:{1}}}", Left, Right);
		}

		public override bool Equals(object obj)
		{
			if (obj != null && obj.GetType() == typeof(GamePadTriggers))
				return this == (GamePadTriggers)obj;

			return false;
		}

		public static bool operator ==(GamePadTriggers lhs, GamePadTriggers rhs)
		{
			return lhs.Left == rhs.Left && lhs.Right == rhs.Right;
		}

		public static bool operator !=(GamePadTriggers lhs, GamePadTriggers rhs)
		{
			return lhs.Left != rhs.Left || lhs.Right != rhs.Right;
		}
	}
}
