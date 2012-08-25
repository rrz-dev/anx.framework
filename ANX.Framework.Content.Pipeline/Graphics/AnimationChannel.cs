#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public sealed class AnimationChannel : ICollection<AnimationKeyframe>, IEnumerable<AnimationKeyframe>, IEnumerable
    {
        List<AnimationKeyframe> frames = new List<AnimationKeyframe>();

        public AnimationChannel()
        {
            
        }

        public int Count
        {
            get { return frames.Count; }
        }

        public AnimationKeyframe this[int index]
        {
            get { return frames[index]; }
        }

        public void Add(AnimationKeyframe item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            frames.Add(item);
        }

        public void Clear()
        {
            frames.Clear();
        }

        public bool Contains(AnimationKeyframe item)
        {
            return frames.Contains(item);
        }

        public IEnumerator<AnimationKeyframe> GetEnumerator()
        {
            return frames.GetEnumerator();
        }

        public int IndexOf(AnimationKeyframe item)
        {
            return frames.IndexOf(item);
        }

        public bool Remove(AnimationKeyframe item)
        {
            return frames.Remove(item);
        }

        public void RemoveAt(int index)
        {
            frames.RemoveAt(index);
        }

        bool ICollection<AnimationKeyframe>.IsReadOnly
        {
            get { return false; }
        }

        void ICollection<AnimationKeyframe>.Add(AnimationKeyframe item)
        {
            frames.Add(item);
        }

        void ICollection<AnimationKeyframe>.CopyTo(AnimationKeyframe[] array, int arrayIndex)
        {
            frames.CopyTo(array, arrayIndex);
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return frames.GetEnumerator();
        }
    }
}
