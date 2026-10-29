using System;
using System.Collections;
using System.Collections.Generic;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input.Touch
{
	[PercentageComplete(100)]
    [Developer("AstrorEnales")]
	[TestState(TestStateAttribute.TestState.Tested)]
	public struct TouchCollection : IList<TouchLocation>, ICollection<TouchLocation>, IEnumerable<TouchLocation>, IEnumerable
	{
		#region Enumerator (helper struct)
		public struct Enumerator : IEnumerator<TouchLocation>, IDisposable, IEnumerator
		{
			private TouchCollection collection;
			private int position;

		    public TouchLocation Current
		    {
		        get { return this.collection[this.position]; }
		    }

		    object IEnumerator.Current
		    {
		        get { return this.Current; }
		    }

		    internal Enumerator(TouchCollection collection)
			{
				this.collection = collection;
				this.position = -1;
			}

			public bool MoveNext()
			{
				this.position++;
				if (this.position >= this.collection.Count)
				{
					this.position = this.collection.Count;
					return false;
				}
				return true;
			}

			void IEnumerator.Reset()
			{
				this.position = -1;
			}

			public void Dispose()
			{
			}
		}
		#endregion

	    private readonly TouchLocation[] locations;
	    private readonly int numberOfUsedTouches;
	    private readonly bool isConnected;

		#region Public
	    public TouchLocation this[int index]
	    {
            get
            {
                if (index < 0 || index >= numberOfUsedTouches)
                    throw new ArgumentOutOfRangeException("index");

                return locations[index];
            }
	        set { throw new NotSupportedException(); }
	    }

	    public int Count
	    {
            get { return numberOfUsedTouches; }
	    }

	    public bool IsConnected
	    {
            get { return isConnected; }
	    }

	    public bool IsReadOnly
	    {
	        get { return true; }
	    }
	    #endregion

		#region Constructor
		public TouchCollection(TouchLocation[] touches)
            : this()
        {
            if (touches == null)
                throw new ArgumentNullException("touches");

            if (touches.Length > 8)
                throw new ArgumentOutOfRangeException("touches");

            locations = new TouchLocation[8];

            for (int index = 0; index < touches.Length; index++)
                locations[index] = touches[index];

		    isConnected = true;
		    numberOfUsedTouches = touches.Length;

            for (int index = numberOfUsedTouches; index < locations.Length; index++)
                locations[index] = default(TouchLocation);
        }
		#endregion

		#region IndexOf
		public int IndexOf(TouchLocation item)
        {
            for (int index = 0; index < numberOfUsedTouches; index++)
                if (this[index] == item)
                    return index;

            return -1;
		}
		#endregion

		#region Unsupported Methods
		public void Insert(int index, TouchLocation item)
        {
            throw new NotSupportedException();
		}

		public void RemoveAt(int index)
        {
            throw new NotSupportedException();
		}

		public void Add(TouchLocation item)
        {
            throw new NotSupportedException();
		}

		public void Clear()
        {
            throw new NotSupportedException();
		}

        public bool Remove(TouchLocation item)
        {
            throw new NotSupportedException();
        }
		#endregion

		#region Contains
		public bool Contains(TouchLocation item)
        {
            return IndexOf(item) >= 0;
		}
		#endregion

		#region CopyTo
		public void CopyTo(TouchLocation[] array, int arrayIndex)
		{
            Array.Copy(locations, 0, array, arrayIndex, Math.Min(locations.Length, array.Length));
		}
		#endregion

		#region FindById
		public bool FindById(int id, out TouchLocation touchLocation)
        {
            for (int index = 0; index < numberOfUsedTouches; index++)
            {
                if (this[index].Id == id)
                {
                    touchLocation = this[index];
                    return true;
                }
            }

			touchLocation = default(TouchLocation);
			return false;
		}
		#endregion

		#region GetEnumerator
		public Enumerator GetEnumerator()
		{
			return new Enumerator(this);
		}

		IEnumerator<TouchLocation> IEnumerable<TouchLocation>.GetEnumerator()
		{
			return new Enumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Enumerator(this);
		}
		#endregion
	}
}
