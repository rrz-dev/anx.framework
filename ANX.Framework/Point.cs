using System;
using System.Globalization;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
		[ANX.Framework.NonXNA.Development.PercentageComplete(100)]
    public struct Point : IEquatable<Point>
    {
        #region fields
        public int X;
        public int Y;
        #endregion

        #region properties
        public static Point Zero
        {
            get
            {
                return new Point(0, 0);
            }
        }
        #endregion

        #region constructors
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        #endregion

        #region public methods
        public override int GetHashCode()
        {
            return this.X + this.Y;
        }

        public override string ToString()
				{
					var culture = CultureInfo.CurrentCulture;
					// This may look a bit more ugly, but String.Format should
					// be avoided cause of it's bad performance!
					return "{X:" + X.ToString(culture) +
						" Y:" + Y.ToString(culture) + "}";

					//return string.Format(culture, "{{X:{0} Y:{1}}}", new object[]
					//{
					//  this.X.ToString(culture), 
					//  this.Y.ToString(culture)
					//});
        }
        #endregion

        #region IEquatable implementation
        public override bool Equals(Object obj)
        {
            return (obj is Point) ? this.Equals((Point)obj) : false;
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
