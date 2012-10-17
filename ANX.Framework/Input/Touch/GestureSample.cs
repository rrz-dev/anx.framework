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
    public struct GestureSample
    {
        private readonly GestureType gestureType;
        private readonly TimeSpan timestamp;
        private readonly Vector2 position;
        private readonly Vector2 position2;
        private readonly Vector2 delta;
        private readonly Vector2 delta2;

        public Vector2 Delta
        {
            get { return this.delta; }
        }

        public Vector2 Delta2
        {
            get { return this.delta2; }
        }

        public GestureType GestureType
        {
            get { return this.gestureType; }
        }

        public Vector2 Position
        {
            get { return this.position; }
        }

        public Vector2 Position2
        {
            get { return this.position2; }
        }

        public TimeSpan Timestamp
        {
            get { return this.timestamp; }
        }

        public GestureSample(GestureType gestureType, TimeSpan timestamp, Vector2 position, Vector2 position2,
            Vector2 delta, Vector2 delta2)
        {
            this.gestureType = gestureType;
            this.timestamp = timestamp;
            this.position = position;
            this.position2 = position2;
            this.delta = delta;
            this.delta2 = delta2;
        }
    }
}
