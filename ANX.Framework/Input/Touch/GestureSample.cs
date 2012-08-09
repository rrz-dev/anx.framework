#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input.Touch
{
    public struct GestureSample
    {
        #region Private Members
        private GestureType gestureType;
        private TimeSpan timestamp;
        private Vector2 position;
        private Vector2 position2;
        private Vector2 delta;
        private Vector2 delta2;

        #endregion // Private Members

        public GestureSample(GestureType gestureType, TimeSpan timestamp, Vector2 position, Vector2 position2, Vector2 delta, Vector2 delta2)
        {
            this.gestureType = gestureType;
            this.timestamp = timestamp;
            this.position = position;
            this.position2 = position2;
            this.delta = delta;
            this.delta2 = delta2;
        }

        public Vector2 Delta
        {
            get
            {
                return this.delta;
            }
        }

        public Vector2 Delta2
        {
            get
            {
                return this.delta2;
            }
        }

        public GestureType GestureType
        {
            get
            {
                return this.gestureType;
            }
        }

        public Vector2 Position
        {
            get
            {
                return this.position;
            }
        }

        public Vector2 Position2
        {
            get
            {
                return this.position2;
            }
        }

        public TimeSpan Timestamp
        {
            get
            {
                return this.timestamp;
            }
        }
    }
}
