using System;
using System.Globalization;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
    [PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Tested)]
    public struct Point : IEquatable<Point>
	{
		#region Constants
		public static Point Zero
		{
			get
			{
				return new Point(0, 0);
			}
		}
		#endregion

		#region Public
		public int X;
		public int Y;
		#endregion

		#region Constructor
		public Point(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}
		#endregion

		#region GetHashCode
		public override int GetHashCode()
		{
			return this.X + this.Y;
		}
		#endregion

		#region ToString
		public override string ToString()
		{
			var culture = CultureInfo.CurrentCulture;
			// This may look a bit more ugly, but String.Format should be avoided cause of it's bad performance!
			return "{X:" + X.ToString(culture) + " Y:" + Y.ToString(culture) + "}";
		}
		#endregion

		#region Equals
		public override bool Equals(Object obj)
		{
			return obj is Point && this.Equals((Point)obj);
		}

		public bool Equals(Point other)
		{
			return this.X == other.X && this.Y == other.Y;
		}
		#endregion

		#region operator overloading
		public static bool operator ==(Point first, Point second)
		{
			return first.X == second.X && first.Y == second.Y;
		}

		public static bool operator !=(Point first, Point second)
		{
			return first.X != second.X || first.Y != second.Y;
		}
		#endregion
	}
}
