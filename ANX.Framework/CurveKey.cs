using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
	public class CurveKey : IEquatable<CurveKey>, IComparable<CurveKey>
	{
		#region Public
		public float Position
		{
			get;
			private set;
		}

		public float Value
		{
			get;
			set;
		}

		public float TangentIn
		{
			get;
			set;
		}

		public float TangentOut
		{
			get;
			set;
		}

		public CurveContinuity Continuity
		{
			get;
			set;
		}
		#endregion

		#region Constructor
		public CurveKey(float position, float value)
			: this(position, value, 0f, 0f, CurveContinuity.Smooth)
		{
		}

		public CurveKey(float position, float value, float tangentIn,
			float tangentOut)
			: this(position, value, tangentIn, tangentOut, CurveContinuity.Smooth)
		{
		}

		public CurveKey(float position, float value, float tangentIn,
			float tangentOut, CurveContinuity continuity)
		{
			Position = position;
			Value = value;
			TangentIn = tangentIn;
			TangentOut = tangentOut;
			Continuity = continuity;
		}
		#endregion

		#region Clone
		public CurveKey Clone()
		{
			return new CurveKey(Position, Value, TangentIn, TangentOut, Continuity);
		}
		#endregion

		#region Equals
        public bool Equals(CurveKey other)
        {
            if (other != null)
            {
                return other.Position == Position &&
                    other.Value == Value &&
                    other.TangentIn == TangentIn &&
                    other.TangentOut == TangentOut &&
                    other.Continuity == Continuity;
            }

            return false;
        }

		public override bool Equals(object obj)
		{
			if (obj == null && obj.GetType() == typeof(CurveKey))
			{
                return this == (CurveKey)obj;
			}

			return false;
		}
		#endregion

		#region GetHashCode
		public override int GetHashCode()
		{
			return Position.GetHashCode() + Value.GetHashCode() +
				TangentIn.GetHashCode() + TangentOut.GetHashCode() +
				Continuity.GetHashCode();
		}
		#endregion

		#region Equality
		public static bool operator ==(CurveKey a, CurveKey b)
		{
            if (null == a as Object)
            {
                return null == b as Object;
            }

            if (null == b as Object)
            {
                return null == a as Object;
            }

            return a.Position == b.Position &&
                   a.Value == b.Value &&
                   a.TangentIn == b.TangentIn &&
                   a.TangentOut == b.TangentOut &&
                   a.Continuity == b.Continuity;
        }
		#endregion

		#region Inequality
		public static bool operator !=(CurveKey a, CurveKey b)
		{
            if (null == a as Object)
            {
                return null != b as Object;
            }

            if (null == b as Object)
            {
                return null != a as Object;
            }

			return a.Position != b.Position ||
			  	   a.Value != b.Value ||
				   a.TangentIn != b.TangentIn ||
				   a.TangentOut != b.TangentOut ||
				   a.Continuity != b.Continuity;
		}
		#endregion

		#region CompareTo
		public int CompareTo(CurveKey other)
		{
			if (Position == other.Position)
			{
				return 0;
			}

			return Position >= other.Position ?
				1 :
				-1;
		}
		#endregion
	}
}
