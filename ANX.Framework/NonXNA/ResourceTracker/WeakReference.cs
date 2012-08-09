#region Using Statements
using System;
using System.Collections.Generic;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
    public sealed class WeakReference<T> where T : class
    {
        private WeakReference target;

        public WeakReference(T target)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            this.target = new WeakReference(target);
        }

        public WeakReference(T target, bool trackResurrection)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            this.target = new WeakReference(target, trackResurrection);
        }

        public T Target
        {
            get
            {
                return target.Target as T;
            }
        }

        public bool IsAlive
        {
            get
            {
                if (target != null)
                {
                    return target.IsAlive;
                }

                return false;
            }
        }

        public bool TrackResurrection
        {
            get
            {
                if (target != null)
                {
                    return target.TrackResurrection;
                }

                return false;
            }
        }


    }
}
