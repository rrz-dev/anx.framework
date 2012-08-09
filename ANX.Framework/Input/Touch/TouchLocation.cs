#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input.Touch
{
    public struct TouchLocation : IEquatable<TouchLocation>
    {
        #region Private members
        private int id;
        private TouchLocationState prevState;
        private Vector2 prevPos;
        private TouchLocationState state;
        private Vector2 pos;

        #endregion // Private members

        public TouchLocation(int id, TouchLocationState state, Vector2 position)
        {
            this.id = id;
            this.state = state;
            this.pos = position;
            this.prevState = TouchLocationState.Invalid;
            this.prevPos = Vector2.Zero;
        }

        public TouchLocation(int id, TouchLocationState state, Vector2 position, TouchLocationState previousState, Vector2 previousPosition)
        {
            this.id = id;
            this.state = state;
            this.pos = position;
            this.prevState = previousState;
            this.prevPos = previousPosition;
        }

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

        public override string ToString()
        {
            return string.Format("{{Position:{0}}}", this.pos);
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode() + this.pos.X.GetHashCode() + this.pos.Y.GetHashCode();
        }

        public override bool Equals(Object other)
        {
            if (other != null && other.GetType() == this.GetType())
            {
                return this == (TouchLocation)other;
            }

            return false;
        }

        public bool Equals(TouchLocation other)
        {
            return this.id == other.id && this.pos == other.pos && this.prevPos == other.prevPos;
        }

        public static bool operator ==(TouchLocation lhs, TouchLocation rhs)
        {
            return lhs.id == rhs.id && lhs.pos == rhs.pos && lhs.prevPos == rhs.prevPos;
        }

        public static bool operator !=(TouchLocation lhs, TouchLocation rhs)
        {
            return lhs.id != rhs.id || lhs.pos != rhs.pos || lhs.prevPos != rhs.prevPos;
        }

        public int Id
        {
            get
            {
                return this.id;
            }
        }

        public Vector2 Position
        {
            get
            {
                return this.pos;
            }
        }

        public TouchLocationState State
        {
            get
            {
                return this.state;
            }
        }
    }
}
