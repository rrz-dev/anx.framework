using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input.Touch
{
	[PercentageComplete(100)]
    [Developer("AstrorEnales")]
	[TestState(TestStateAttribute.TestState.Tested)]
    public struct TouchLocation : IEquatable<TouchLocation>
    {
        #region Private
        private int id;
        private TouchLocationState prevState;
        private Vector2 prevPos;
        private TouchLocationState state;
        private Vector2 pos;
		#endregion

		#region Public
	    public int Id
	    {
	        get { return id; }
	    }

	    public Vector2 Position
	    {
	        get { return pos; }
	    }

	    public TouchLocationState State
	    {
	        get { return state; }
	    }
	    #endregion

		#region Constructor
		public TouchLocation(int id, TouchLocationState state, Vector2 position)
        {
            this.id = id;
            this.state = state;
            this.pos = position;
            this.prevState = TouchLocationState.Invalid;
            this.prevPos = Vector2.Zero;
        }

        public TouchLocation(int id, TouchLocationState state, Vector2 position, TouchLocationState previousState,
			Vector2 previousPosition)
        {
            this.id = id;
            this.state = state;
            this.pos = position;
            this.prevState = previousState;
            this.prevPos = previousPosition;
        }
		#endregion

		#region TryGetPreviousLocation
		public bool TryGetPreviousLocation(out TouchLocation previousLocation)
        {
            if (this.prevState == TouchLocationState.Invalid)
            {
                previousLocation.id = -1;
                previousLocation.state = TouchLocationState.Invalid;
                previousLocation.pos = Vector2.Zero;
                previousLocation.prevState = TouchLocationState.Invalid;
                previousLocation.prevPos = Vector2.Zero;
            
                return false;
            }

            previousLocation.id = this.id;
            previousLocation.state = this.prevState;
            previousLocation.pos = this.pos;
            previousLocation.prevState = TouchLocationState.Invalid;
            previousLocation.prevPos = this.prevPos;
            
            return true;
        }
		#endregion
		
		#region ToString
        public override string ToString()
        {
            return string.Format("{{Position:{0}}}", pos);
        }
		#endregion
		
		#region GetHashCode
        public override int GetHashCode()
        {
            return id.GetHashCode() + pos.X.GetHashCode() + pos.Y.GetHashCode();
        }
		#endregion
		
		#region Equals
        public override bool Equals(object other)
        {
            return other is TouchLocation && this == (TouchLocation)other;
        }

	    public bool Equals(TouchLocation other)
        {
            return id == other.id && pos == other.pos && prevPos == other.prevPos;
        }
		#endregion

		#region Operators
        public static bool operator ==(TouchLocation lhs, TouchLocation rhs)
        {
            return lhs.id == rhs.id && lhs.pos == rhs.pos && lhs.prevPos == rhs.prevPos;
        }

        public static bool operator !=(TouchLocation lhs, TouchLocation rhs)
        {
            return lhs.id != rhs.id || lhs.pos != rhs.pos || lhs.prevPos != rhs.prevPos;
        }
		#endregion
    }
}
