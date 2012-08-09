#region Using Statements
using System;
using System.Collections.Generic;
using System.Collections;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.GamerServices
{
    public sealed class AchievementCollection : IList<Achievement>, ICollection<Achievement>,
			IEnumerable<Achievement>, IEnumerable, IDisposable
    {
        public int IndexOf(Achievement item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, Achievement item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public Achievement this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(Achievement item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Achievement item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Achievement[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(Achievement item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Achievement> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
			
        public bool IsDisposed
        {
            get { throw new NotImplementedException(); }
        }
			
        public Achievement this[string name]
        {
            get { throw new NotImplementedException(); }
        }
    }
}
