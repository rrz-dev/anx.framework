using System;
using System.Globalization;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
    [PercentageComplete(100)]
    [Developer("???")]
    [TestState(TestStateAttribute.TestState.InProgress)]
    public struct Rectangle : IEquatable<Rectangle>
    {
        #region fields
        public int X;
        public int Y;
        public int Width;
        public int Height;
        #endregion

        #region properties
        public Point Center
        {
            get
            {
                return new Point((int)(X + Width * 0.5f), (int)(Y + Height * 0.5f));
            }
        }

        public static Rectangle Empty
        {
            get
            {
                return new Rectangle();
            }
        }

        public bool IsEmpty
        {
            get
            {
                return (this.X == 0) &&
									(this.Y == 0) &&
									(this.Width == 0) &&
									(this.Height == 0);
            }
        }

        public Point Location
        {
            get
            {
                return new Point(this.X, this.Y);
            }
            set
            {
                this.X = value.X;
                this.Y = value.Y;
            }
        }

        public int Left
        {
            get
            {
                return this.X;
            }
        }

        public int Right
        {
            get
            {
                return this.X + Width;
            }
        }

        public int Top
        {
            get
            {
                return this.Y;
            }
        }

        public int Bottom
        {
            get
            {
                return this.Y + Height;
            }
        }
        #endregion

        #region constructors
        public Rectangle(int x, int y, int width, int height)
        {
            this.Height = height;
            this.Width = width;
            this.X = x;
            this.Y = y;
        }
        #endregion

        #region public methods
        public bool Contains(int x, int y)
        {
            bool result;
            this.Contains(ref x, ref y, out result);
            return result;
        }

        public bool Contains(Point value)
        {
            bool result;
            this.Contains(ref value.X, ref value.Y, out result);
            return result;

        }
        public void Contains(ref Point value, out bool result)
        {
            this.Contains(ref value.X, ref value.Y, out result);
        }

        private void Contains(ref int x, ref int y, out bool result)
        {
            result = x > this.X &&
                     x < this.Right &&
                     y > this.Y &&
                     y < this.Bottom;
        }

        public bool Contains(Rectangle value)
        {
            bool result;
            this.Contains(ref value, out result);
            return result;
        }

        public void Contains(ref Rectangle value, out bool result)
        {
            result = value.X >= this.X &&
						value.X + value.Width <= this.Right &&
            value.Y >= this.Y &&
            value.Y + this.Height <= this.Bottom;
        }

        public override int GetHashCode()
        {
            return this.X + this.Y + this.Width + this.Height;
        }

        public void Inflate(int horizontalAmount, int verticalAmount)
        {
            this.X -= horizontalAmount;
            this.Y -= verticalAmount;
            this.Width += horizontalAmount * 2;
            this.Height += verticalAmount * 2;
        }

        public bool Intersects(Rectangle value)
        {
            bool result;
            this.Intersects(ref value, out result);
            return result;
        }

        public void Intersects(ref Rectangle value, out bool result)
        {
            //intersects if it dont contains it and is not outer

            //outer
            if (value.X > this.Right ||
                value.Y > this.Bottom ||
                value.X + value.Width < this.X ||
                value.Y + value.Height < this.Y)
            {
                result = false;
                return;
            }
            //contains
            if (this.Contains(value))
            {
                result = false;
                return;
            }
            result = true;
        }

        public void Offset(int offsetX, int offsetY)
        {
            this.X += offsetX;
            this.Y += offsetY;

        }
        public void Offset(Point amount)
        {
            this.X += amount.X;
            this.Y += amount.Y;

        }

        public override string ToString()
        {
            var culture = CultureInfo.CurrentCulture;
            // This may look a bit more ugly, but String.Format should
            // be avoided cause of it's bad performance!
            return "{X:" + X.ToString(culture) +
                " Y:" + Y.ToString(culture) +
                " Width:" + Width.ToString(culture) +
                " Height:" + Height.ToString(culture) + "}";
        }

        #endregion

        #region static methods
        public static Rectangle Intersect(Rectangle value1, Rectangle value2)
        {
            Rectangle result;
            Rectangle.Intersect(ref value1, ref value2, out result);
            return result;
        }

        public static void Intersect(ref Rectangle value1, ref Rectangle value2, out Rectangle result)
        {
            result = new Rectangle();
            int x, y, w, h;
            if (value1.X > value2.X)
            {
                if (value1.X < value2.X + value2.Width)
                {
                    x = value1.X;
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (value2.X < value1.X + value1.Width)
                {
                    x = value2.X;
                }
                else
                {
                    return;
                }
            }

            if (value1.Y > value2.Y)
            {
                if (value1.Y < value2.Y + value2.Height)
                {
                    y = value1.Y;
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (value2.Y < value1.Y + value1.Height)
                {
                    y = value2.Y;
                }
                else
                {
                    return;
                }
            }

            if (value1.X + value1.Width < value2.X + value2.Width)
            {
                if (value1.X + value1.Width > value2.X)
                {
                    w = value1.Width;
                }
                else
                {
                    return;
                }

            }
            else
            {
                if (value2.X + value2.Width > value1.X)
                {
                    w = value2.Width;
                }
                else
                {
                    return;
                }
            }

            if (value1.Y + value1.Height < value2.Y + value2.Height)
            {
                if (value1.Y + value1.Height > value2.Y)
                {
                    h = value1.Height;
                }
                else
                {
                    return;
                }

            }
            else
            {
                if (value2.Y + value2.Height > value1.Y)
                {
                    h = value2.Height;
                }
                else
                {
                    return;
                }
            }

            result = new Rectangle(x, y, w-x, h-y);

        }
        public static Rectangle Union(Rectangle value1, Rectangle value2)
        {
            Rectangle result;
            Rectangle.Union(ref value1, ref value2, out result);
            return result;
        }

        public static void Union(ref Rectangle value1, ref Rectangle value2, out Rectangle result)
        {
            //Pick smallest x and y
            int x = value1.X < value2.X ? value1.X : value2.X;
            int y = value1.Y < value2.Y ? value1.Y : value2.Y;

            //pick greatest height and width
            int w = value1.X + value1.Width > value2.X + value2.Width ? value1.X + value1.Width : value2.X + value2.Width;
            int h = value1.Y + value1.Height > value2.Y + value2.Height ? value1.Y + value1.Height : value2.Y + value2.Height;

            result = new Rectangle(x, y, w-x, h-y);
        }

        #endregion

        #region IEquatable implementation
        public override bool Equals(Object obj)
        {
            return (obj is Rectangle) ? this.Equals((Rectangle)obj) : false;
        }
        public bool Equals(Rectangle other)
        {
            return this.Height == other.Height &&
							this.Width == other.Width &&
							this.X == other.X &&
							this.Y == other.Y;
        }
        #endregion

        #region operator overloading
        public static bool operator ==(Rectangle a, Rectangle b)
        {
            // NOTE: Duplicated code is better than copying 4 floats around!
            return a.Height == b.Height &&
                a.Width == b.Width &&
                a.X == b.X &&
                a.Y == b.Y;
        }
        public static bool operator !=(Rectangle a, Rectangle b)
        {
            // NOTE: Duplicated code is better than copying 4 floats around!
            return a.Height != b.Height ||
                a.Width != b.Width ||
                a.X != b.X ||
                a.Y != b.Y;
        }
        #endregion
    }
}
