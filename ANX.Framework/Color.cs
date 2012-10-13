using System;
using ANX.Framework.Graphics.PackedVector;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
    [PercentageComplete(100)]
    [Developer("???")]
    [TestState(TestStateAttribute.TestState.InProgress)]
    public struct Color : IPackedVector<uint>, IPackedVector, IEquatable<Color>
    {
        #region Private Members
			/// <summary>
			/// Marcel NOTE: I made this internal and befriend the ANX.Framework
			/// with the OGL Module so the datatype conversion can be made faster.
			/// </summary>
        internal uint packedValue;

        #endregion // Private Members

        #region Constructors

        public Color(int r, int g, int b)
        {
            if ((((r | g) | b) & -256) != 0)
            {
                r = r < 0 ? 0 : (r > 255 ? 255 : r);
                g = g < 0 ? 0 : (g > 255 ? 255 : g);
                b = b < 0 ? 0 : (b > 255 ? 255 : b);
            }

            this.packedValue = (uint)(((r | g << 8) | b << 16) | -16777216);
        }

        public Color(int r, int g, int b, int a)
        {
            if (((((r | g) | b) | a) & -256) != 0)
            {
                r = r < 0 ? 0 : (r > 255 ? 255 : r);
                g = g < 0 ? 0 : (g > 255 ? 255 : g);
                b = b < 0 ? 0 : (b > 255 ? 255 : b);
                a = a < 0 ? 0 : (a > 255 ? 255 : a);
            }

            this.packedValue = (uint)(((r | g << 8) | b << 16) | a << 24);
        }

        public Color(float r, float g, float b)
        {
            this.packedValue = ColorPack(r, g, b, 1.0f);
        }

        public Color(float r, float g, float b, float a)
        {
            this.packedValue = ColorPack(r, g, b, a);
        }

        public Color(Vector3 vector)
            : this(vector.X, vector.Y, vector.Z)
        {
        }

        public Color(Vector4 vector)
            : this(vector.X, vector.Y, vector.Z, vector.W)
        {
        }

        private Color(uint packedValue)
        {
            this.packedValue = packedValue;
        }

        #endregion // Constructors

        #region Color Constants

        public static Color AliceBlue
        {
            get { return new Color(240, 248, 255, 255); }
        }

        public static Color AntiqueWhite
        {
            get { return new Color(250, 235, 215, 255); }
        }

        public static Color Aqua
        {
            get { return new Color(0, 255, 255, 255); }
        }

        public static Color Aquamarine
        {
            get { return new Color(127, 255, 212, 255); }
        }

        public static Color Azure
        {
            get { return new Color(240, 255, 255, 255); }
        }

        public static Color Beige
        {
            get { return new Color(245, 245, 220, 255); }
        }

        public static Color Bisque
        {
            get { return new Color(255, 228, 196, 255); }
        }

        public static Color Black
        {
            get { return new Color(0, 0, 0, 255); }
        }

        public static Color BlanchedAlmond
        {
            get { return new Color(255, 235, 205, 255); }
        }

        public static Color Blue
        {
            get { return new Color(0, 0, 255, 255); }
        }

        public static Color BlueViolet
        {
            get { return new Color(138, 43, 226, 255); }
        }

        public static Color Brown
        {
            get { return new Color(165, 42, 42, 255); }
        }

        public static Color BurlyWood
        {
            get { return new Color(222, 184, 135, 255); }
        }

        public static Color CadetBlue
        {
            get { return new Color(95, 158, 160, 255); }
        }

        public static Color Chartreuse
        {
            get { return new Color(127, 255, 0, 255); }
        }

        public static Color Chocolate
        {
            get { return new Color(210, 105, 30, 255); }
        }

        public static Color Coral
        {
            get { return new Color(255, 127, 80, 255); }
        }

        public static Color CornflowerBlue
        {
            get { return new Color(0xffed9564); }
        }

        public static Color Cornsilk
        {
            get { return new Color(255, 248, 220, 255); }
        }

        public static Color Crimson
        {
            get { return new Color(220, 20, 60, 255); }
        }

        public static Color Cyan
        {
            get { return new Color(0, 255, 255, 255); }
        }

        public static Color DarkBlue
        {
            get { return new Color(0, 0, 139, 255); }
        }

        public static Color DarkCyan
        {
            get { return new Color(0, 139, 139, 255); }
        }

        public static Color DarkGoldenrod
        {
            get { return new Color(184, 134, 11, 255); }
        }

        public static Color DarkGray
        {
            get { return new Color(169, 169, 169, 255); }
        }

        public static Color DarkGreen
        {
            get { return new Color(0, 100, 0, 255); }
        }

        public static Color DarkKhaki
        {
            get { return new Color(189, 183, 107, 255); }
        }

        public static Color DarkMagenta
        {
            get { return new Color(139, 0, 139, 255); }
        }

        public static Color DarkOliveGreen
        {
            get { return new Color(85, 107, 47, 255); }
        }

        public static Color DarkOrange
        {
            get { return new Color(255, 140, 0, 255); }
        }

        public static Color DarkOrchid
        {
            get { return new Color(153, 50, 204, 255); }
        }

        public static Color DarkRed
        {
            get { return new Color(139, 0, 0, 255); }
        }

        public static Color DarkSalmon
        {
            get { return new Color(233, 150, 122, 255); }
        }

        public static Color DarkSeaGreen
        {
            get { return new Color(143, 188, 139, 255); }
        }

        public static Color DarkSlateBlue
        {
            get { return new Color(72, 61, 139, 255); }
        }

        public static Color DarkSlateGray
        {
            get { return new Color(47, 79, 79, 255); }
        }

        public static Color DarkTurquoise
        {
            get { return new Color(0, 206, 209, 255); }
        }

        public static Color DarkViolet
        {
            get { return new Color(148, 0, 211, 255); }
        }

        public static Color DeepPink
        {
            get { return new Color(255, 20, 147, 255); }
        }

        public static Color DeepSkyBlue
        {
            get { return new Color(0, 191, 255, 255); }
        }

        public static Color DimGray
        {
            get { return new Color(105, 105, 105, 255); }
        }

        public static Color DodgerBlue
        {
            get { return new Color(30, 144, 255, 255); }
        }

        public static Color Firebrick
        {
            get { return new Color(178, 34, 34, 255); }
        }

        public static Color FloralWhite
        {
            get { return new Color(255, 250, 240, 255); }
        }

        public static Color ForestGreen
        {
            get { return new Color(34, 139, 34, 255); }
        }

        public static Color Fuchsia
        {
            get { return new Color(255, 0, 255, 255); }
        }

        public static Color Gainsboro
        {
            get { return new Color(220, 220, 220, 255); }
        }

        public static Color GhostWhite
        {
            get { return new Color(248, 248, 255, 255); }
        }

        public static Color Gold
        {
            get { return new Color(255, 215, 0, 255); }
        }

        public static Color Goldenrod
        {
            get { return new Color(218, 165, 32, 255); }
        }

        public static Color Gray
        {
            get { return new Color(128, 128, 128, 255); }
        }

        public static Color Green
        {
            get { return new Color(0, 128, 0, 255); }
        }

        public static Color GreenYellow
        {
            get { return new Color(173, 255, 47, 255); }
        }

        public static Color Honeydew
        {
            get { return new Color(240, 255, 240, 255); }
        }

        public static Color HotPink
        {
            get { return new Color(255, 105, 180, 255); }
        }

        public static Color IndianRed
        {
            get { return new Color(205, 92, 92, 255); }
        }

        public static Color Indigo
        {
            get { return new Color(75, 0, 130, 255); }
        }

        public static Color Ivory
        {
            get { return new Color(255, 255, 240, 255); }
        }

        public static Color Khaki
        {
            get { return new Color(240, 230, 140, 255); }
        }

        public static Color Lavender
        {
            get { return new Color(230, 230, 250, 255); }
        }

        public static Color LavenderBlush
        {
            get { return new Color(255, 240, 245, 255); }
        }

        public static Color LawnGreen
        {
            get { return new Color(124, 252, 0, 255); }
        }

        public static Color LemonChiffon
        {
            get { return new Color(255, 250, 205, 255); }
        }

        public static Color LightBlue
        {
            get { return new Color(173, 216, 230, 255); }
        }

        public static Color LightCoral
        {
            get { return new Color(240, 128, 128, 255); }
        }

        public static Color LightCyan
        {
            get { return new Color(224, 255, 255, 255); }
        }

        public static Color LightGoldenrodYellow
        {
            get { return new Color(250, 250, 210, 255); }
        }

        public static Color LightGray
        {
            get { return new Color(211, 211, 211, 255); }
        }

        public static Color LightGreen
        {
            get { return new Color(144, 238, 144, 255); }
        }

        public static Color LightPink
        {
            get { return new Color(255, 182, 193, 255); }
        }

        public static Color LightSalmon
        {
            get { return new Color(255, 160, 122, 255); }
        }

        public static Color LightSeaGreen
        {
            get { return new Color(32, 178, 170, 255); }
        }

        public static Color LightSkyBlue
        {
            get { return new Color(135, 206, 250, 255); }
        }

        public static Color LightSlateGray
        {
            get { return new Color(119, 136, 153, 255); }
        }

        public static Color LightSteelBlue
        {
            get { return new Color(176, 196, 222, 255); }
        }

        public static Color LightYellow
        {
            get { return new Color(255, 255, 224, 255); }
        }

        public static Color Lime
        {
            get { return new Color(0, 255, 0, 255); }
        }

        public static Color LimeGreen
        {
            get { return new Color(50, 205, 50, 255); }
        }

        public static Color Linen
        {
            get { return new Color(250, 240, 230, 255); }
        }

        public static Color Magenta
        {
            get { return new Color(255, 0, 255, 255); }
        }

        public static Color Maroon
        {
            get { return new Color(128, 0, 0, 255); }
        }

        public static Color MediumAquamarine
        {
            get { return new Color(102, 205, 170, 255); }
        }

        public static Color MediumBlue
        {
            get { return new Color(0, 0, 205, 255); }
        }

        public static Color MediumOrchid
        {
            get { return new Color(186, 85, 211, 255); }
        }

        public static Color MediumPurple
        {
            get { return new Color(147, 112, 219, 255); }
        }

        public static Color MediumSeaGreen
        {
            get { return new Color(60, 179, 113, 255); }
        }

        public static Color MediumSlateBlue
        {
            get { return new Color(123, 104, 238, 255); }
        }

        public static Color MediumSpringGreen
        {
            get { return new Color(0, 250, 154, 255); }
        }

        public static Color MediumTurquoise
        {
            get { return new Color(72, 209, 204, 255); }
        }

        public static Color MediumVioletRed
        {
            get { return new Color(199, 21, 133, 255); }
        }

        public static Color MidnightBlue
        {
            get { return new Color(25, 25, 112, 255); }
        }

        public static Color MintCream
        {
            get { return new Color(245, 255, 250, 255); }
        }

        public static Color MistyRose
        {
            get { return new Color(255, 228, 225, 255); }
        }

        public static Color Moccasin
        {
            get { return new Color(255, 228, 181, 255); }
        }

        public static Color NavajoWhite
        {
            get { return new Color(255, 222, 173, 255); }
        }

        public static Color Navy
        {
            get { return new Color(0, 0, 128, 255); }
        }

        public static Color OldLace
        {
            get { return new Color(253, 245, 230, 255); }
        }

        public static Color Olive
        {
            get { return new Color(128, 128, 0, 255); }
        }

        public static Color OliveDrab
        {
            get { return new Color(107, 142, 35, 255); }
        }

        public static Color Orange
        {
            get { return new Color(255, 165, 0, 255); }
        }

        public static Color OrangeRed
        {
            get { return new Color(255, 69, 0, 255); }
        }

        public static Color Orchid
        {
            get { return new Color(218, 112, 214, 255); }
        }

        public static Color PaleGoldenrod
        {
            get { return new Color(238, 232, 170, 255); }
        }

        public static Color PaleGreen
        {
            get { return new Color(152, 251, 152, 255); }
        }

        public static Color PaleTurquoise
        {
            get { return new Color(175, 238, 238, 255); }
        }

        public static Color PaleVioletRed
        {
            get { return new Color(219, 112, 147, 255); }
        }

        public static Color PapayaWhip
        {
            get { return new Color(255, 239, 213, 255); }
        }

        public static Color PeachPuff
        {
            get { return new Color(255, 218, 185, 255); }
        }

        public static Color Peru
        {
            get { return new Color(205, 133, 63, 255); }
        }

        public static Color Pink
        {
            get { return new Color(255, 192, 203, 255); }
        }

        public static Color Plum
        {
            get { return new Color(221, 160, 221, 255); }
        }

        public static Color PowderBlue
        {
            get { return new Color(176, 224, 230, 255); }
        }

        public static Color Purple
        {
            get { return new Color(128, 0, 128, 255); }
        }

        public static Color Red
        {
            get { return new Color(255, 0, 0, 255); }
        }

        public static Color RosyBrown
        {
            get { return new Color(188, 143, 143, 255); }
        }

        public static Color RoyalBlue
        {
            get { return new Color(65, 105, 225, 255); }
        }

        public static Color SaddleBrown
        {
            get { return new Color(139, 69, 19, 255); }
        }

        public static Color Salmon
        {
            get { return new Color(250, 128, 114, 255); }
        }

        public static Color SandyBrown
        {
            get { return new Color(244, 164, 96, 255); }
        }

        public static Color SeaGreen
        {
            get { return new Color(46, 139, 87, 255); }
        }

        public static Color SeaShell
        {
            get { return new Color(255, 245, 238, 255); }
        }

        public static Color Sienna
        {
            get { return new Color(160, 82, 45, 255); }
        }

        public static Color Silver
        {
            get { return new Color(192, 192, 192, 255); }
        }

        public static Color SkyBlue
        {
            get { return new Color(135, 206, 235, 255); }
        }

        public static Color SlateBlue
        {
            get { return new Color(106, 90, 205, 255); }
        }

        public static Color SlateGray
        {
            get { return new Color(112, 128, 144, 255); }
        }

        public static Color Snow
        {
            get { return new Color(255, 250, 250, 255); }
        }

        public static Color SpringGreen
        {
            get { return new Color(0, 255, 127, 255); }
        }

        public static Color SteelBlue
        {
            get { return new Color(70, 130, 180, 255); }
        }

        public static Color Tan
        {
            get { return new Color(210, 180, 140, 255); }
        }

        public static Color Teal
        {
            get { return new Color(0, 128, 128, 255); }
        }

        public static Color Thistle
        {
            get { return new Color(216, 191, 216, 255); }
        }

        public static Color Tomato
        {
            get { return new Color(255, 99, 71, 255); }
        }

		public static Color Transparent
		{
			get { return new Color(0, 0, 0, 0); }
		}

        public static Color Turquoise
        {
            get { return new Color(64, 224, 208, 255); }
        }

        public static Color Violet
        {
            get { return new Color(238, 130, 238, 255); }
        }

        public static Color Wheat
        {
            get { return new Color(245, 222, 179, 255); }
        }

        public static Color White
        {
            get { return new Color(255, 255, 255, 255); }
        }

        public static Color WhiteSmoke
        {
            get { return new Color(245, 245, 245, 255); }
        }

        public static Color Yellow
        {
            get { return new Color(255, 255, 0, 255); }
        }

        public static Color YellowGreen
        {
            get { return new Color(154, 205, 50, 255); }
        }

        #endregion // Color Constants

        #region Methods
        public bool Equals(Color other)
        {
            return this.packedValue.Equals(other.packedValue);
        }

        public override bool Equals(Object other)
        {
            if (other is Color)
            {
                return this.Equals((Color)other);
            }

            return false;
        }

        public static bool operator ==(Color a, Color b)
        {
					return a.packedValue == b.packedValue;
        }

        public static bool operator !=(Color a, Color b)
        {
					return a.packedValue != b.packedValue;
        }

        public static Color FromNonPremultiplied(int r, int g, int b, int a)
        {
            Color color;

            r = ClampValue64((long)r * a / 255L);
            g = ClampValue64((long)g * a / 255L);
            b = ClampValue64((long)b * a / 255L);
            a = ClampValue32(a);

            /* //What the heck is this? Anyway it does not work!
            if (((((r | g) | b) | a) & -256) != 0)
            {
                r = r < 0 ? 0 : (r > 255 ? 255 : r);
                g = g < 0 ? 0 : (g > 255 ? 255 : g);
                b = b < 0 ? 0 : (b > 255 ? 255 : b);
                a = a < 0 ? 0 : (a > 255 ? 255 : a);
            }*/

            color.packedValue = (uint)(((r | g << 8) | b << 16) | a << 24);

            return color;
        }

        private static int ClampValue64(long value)
        {
            if (value < 0L)
                return 0;
            if (value > 255L)
                return 255;
            return (int) value;
        }

        public static int ClampValue32(int value)
        {
            if (value < 0)
                return 0;
            if (value > 255)
                return 255;
            return value;
        }

        public static Color FromNonPremultiplied(Vector4 vector)
        {
            Color color;

            color.packedValue = ColorPack(vector.X * vector.W, vector.Y * vector.W, vector.Z * vector.W, vector.W);

            return color;
        }

        public override int GetHashCode()
        {
            return this.packedValue.GetHashCode();
        }

				#region Lerp
        public static Color Lerp(Color value1, Color value2, float amount)
        {
            Color color;

            byte r1 = (byte) value1.packedValue;
            byte g1 = (byte) (value1.packedValue >> 8);
            byte b1 = (byte) (value1.packedValue >> 16);
            byte a1 = (byte) (value1.packedValue >> 24);

            byte r2 = (byte) value2.packedValue;
            byte g2 = (byte) (value2.packedValue >> 8);
            byte b2 = (byte) (value2.packedValue >> 16);
            byte a2 = (byte) (value2.packedValue >> 24);

            int factor = (int) PackUNormal(65536f, amount);

            int r3 = r1 + (((r2 - r1)*factor) >> 16);
            int g3 = g1 + (((g2 - g1)*factor) >> 16);
            int b3 = b1 + (((b2 - b1)*factor) >> 16);
            int a3 = a1 + (((a2 - a1)*factor) >> 16);

            color.packedValue = (uint) (((r3 | (g3 << 8)) | (b3 << 16)) | (a3 << 24));

            return color;
        }

        #endregion

		#region Multiply
        public static Color Multiply(Color value, float scale)
        {
            Color color;

            uint r = (byte) value.packedValue;
            uint g = (byte) (value.packedValue >> 8);
            uint b = (byte) (value.packedValue >> 16);
            uint a = (byte) (value.packedValue >> 24);

            uint uintScale = (uint) MathHelper.Clamp(scale*65536f, 0, 0xffffff);

            r = (r*uintScale) >> 16;
            g = (g*uintScale) >> 16;
            b = (b*uintScale) >> 16;
            a = (a*uintScale) >> 16;

            r = r > 255 ? 255 : r;
            g = g > 255 ? 255 : g;
            b = b > 255 ? 255 : b;
            a = a > 255 ? 255 : a;

            color.packedValue = ((r | (g << 8)) | (b << 0x10)) | (a << 0x18);

            return color;
        }

        #endregion

        public static Color operator *(Color a, float scale)
        {
            return Multiply(a, scale);
        }

        public override string ToString()
		{
					// This may look a bit more ugly, but String.Format should
					// be avoided cause of it's bad performance!
					return "{R:" + R + " G:" + G + " B:" + B + " A:" + A + "}";

					//return string.Format(CultureInfo.CurrentCulture, "{{R:{0} G:{1} B:{2} A:{3}}}", new object[] { this.R, this.G, this.B, this.A });
        }

        public Vector3 ToVector3()
        {
            Vector3 result;

            result.X = (packedValue & 255);
            result.Y = (packedValue >> 8 & 255);
            result.Z = (packedValue >> 16 & 255);

            result /= 0xff;

            return result;
        }

        public Vector4 ToVector4()
        {
            Vector4 result;

            result.X = (packedValue & 255);
            result.Y = (packedValue >> 8 & 255);
            result.Z = (packedValue >> 16 & 255);
            result.W = (packedValue >> 24 & 255);

            result /= 0xff;

            return result;
        }

        #endregion // Methods

        #region Properties

        public uint PackedValue
        {
            get
            {
                return this.packedValue;
            }
            set
            {
                this.packedValue = value;
            }
        }

        public byte R
        {
            get
            {
                return (byte)this.packedValue;
            }
            set
            {
                this.packedValue = (this.packedValue & 0xffffff00) | value;
            }
        }

        public byte G
        {
            get
            {
                return (byte) (this.packedValue >> 8);
            }
            set
            {
                this.packedValue = (this.packedValue & 0xffff00ff) | ((uint) (value << 8));
            }
        }

        public byte B
        {
            get
            {
                return (byte)(this.packedValue >> 0x10);
            }
            set
            {
                this.packedValue = (this.packedValue & 0xff00ffff) | ((uint)(value << 0x10));
            }
        }

        public byte A
        {
            get
            {
                return (byte)(this.packedValue >> 0x18);
            }
            set
            {
                this.packedValue = (this.packedValue & 0xffffff) | ((uint)(value << 0x18));
            }
        }

        #endregion // Properties

        #region Helper
        private static uint ColorPack(float r, float g, float b, float a)
        {
            uint pr = PackUNormal(255f, r);
            uint pg = PackUNormal(255f, g) << 8;
            uint pb = PackUNormal(255f, b) << 16;
            uint pa = PackUNormal(255f, a) << 24;

            return (((pr | pg) | pb) | pa);
        }

        private static uint PackUNormal(float bitmask, float value)
        {
            value *= bitmask;

            if (float.IsNaN(value))
            {
                return 0;
            }
            if (float.IsInfinity(value))
            {
                return (uint)(float.IsNegativeInfinity(value) ? 0 : bitmask);
            }
            if (value < 0)
            {
                return 0;
            }

            return (uint)value;
        }

        void IPackedVector.PackFromVector4(Vector4 vector)
        {
            this.packedValue = ColorPack(vector.X, vector.Y, vector.Z, vector.W);
        }

        #endregion // Helper
    }
}
