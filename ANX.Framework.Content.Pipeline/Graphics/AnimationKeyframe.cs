#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public sealed class AnimationKeyframe : IComparable<AnimationKeyframe>
    {
        private TimeSpan time;
        private Matrix transform;

        public AnimationKeyframe(TimeSpan time, Matrix transform)
        {
            this.time = time;
            this.transform = transform;
        }

        public TimeSpan Time
        {
            get
            {
                return this.time;
            }
        }

        public Matrix Transform
        {
            get
            {
                return this.transform;
            }
            set
            {
                this.transform = value;
            }
        }

        public int CompareTo(AnimationKeyframe other)
        {
            return time.CompareTo(other.time);
        }
    }
}
