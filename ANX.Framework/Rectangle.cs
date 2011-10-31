#region Using Statements
using System;

#endregion // Using Statements

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

namespace ANX.Framework
{
    public struct Rectangle : IEquatable<Rectangle>
    {
        #region fields
        public int Height;
        public int Width;
        public int X;
        public int Y;
        #endregion


        #region properties
        public int Bottom
        {
            get
            {
                return this.Y + Height;
            }
        }
        public Point Center
        {
            get
            {
                float faktor = 1 / 2f;
                return new Point((int)(this.X + this.Width * faktor), (int)(this.Y + this.Height * faktor));
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
                return (this.X == 0) && (this.Y == 0) & (this.Width == 0) && (this.Height == 0);
            }
        }
        public int Left
        {
            get
            {
                return this.X;
            }
        }
        public Point Location
        {
            get
            {
                return new Point(this.Left, this.Top);
            }
            set
            {
                this.X = value.X;
                this.Y = value.Y;
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
        x < this.X + this.Width &&
        y > this.Y &&
        y < this.Y + this.Height;
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
            value.X + value.Width <= this.X + this.Width &&
            value.Y >= this.Y &&
            value.Y + this.Height <= this.Y + this.Height;
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
            if (value.X > this.X + this.Width ||
            value.Y > this.Y + this.Height ||
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
            return "{X:" + this.X + " Y:" + this.Y + " Width:" + this.Width + " Height:" + this.Height + "}";
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
            return this.Height == other.Height && this.Width == other.Width && this.X == other.X && this.Y == other.Y;
        }
        #endregion


        #region operator overloading
        public static bool operator ==(Rectangle a, Rectangle b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Rectangle a, Rectangle b)
        {
            return !a.Equals(b);
        }
        #endregion
    }
}
