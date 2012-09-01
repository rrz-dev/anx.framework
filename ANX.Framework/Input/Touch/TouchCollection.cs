using System;
using System.Collections;
using System.Collections.Generic;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input.Touch
{
	[PercentageComplete(90)]
	[TestState(TestStateAttribute.TestState.Untested)]
	public struct TouchCollection : IList<TouchLocation>, ICollection<TouchLocation>, IEnumerable<TouchLocation>, IEnumerable
	{
		#region Enumerator (helper struct)
		public struct Enumerator : IEnumerator<TouchLocation>, IDisposable, IEnumerator
		{
			private TouchCollection collection;
			private int position;

			public TouchLocation Current
			{
				get
				{
					return this.collection[this.position];
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
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

		#region Private
		private List<TouchLocation> locations;
		#endregion

		#region Public (TODO)
		public TouchLocation this[int index]
		{
			get
			{
				return locations[index];
			}
			set
			{
				locations[index] = value;
			}
		}

		public int Count
		{
			get
			{
				return locations.Count;
			}
		}

		public bool IsConnected
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool IsReadOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		#endregion

		#region Constructor
		public TouchCollection(TouchLocation[] touches)
		{
			locations = new List<TouchLocation>(touches);
		}
		#endregion

		#region IndexOf
		public int IndexOf(TouchLocation item)
		{
			return locations.IndexOf(item);
		}
		#endregion

		#region Insert
		public void Insert(int index, TouchLocation item)
		{
			locations.Insert(index, item);
		}
		#endregion

		#region RemoveAt
		public void RemoveAt(int index)
		{
			locations.RemoveAt(index);
		}
		#endregion

		#region Add
		public void Add(TouchLocation item)
		{
			locations.Add(item);
		}
		#endregion

		#region Clear
		public void Clear()
		{
			locations.Clear();
		}
		#endregion

		#region Contains
		public bool Contains(TouchLocation item)
		{
			return locations.Contains(item);
		}
		#endregion

		#region CopyTo
		public void CopyTo(TouchLocation[] array, int arrayIndex)
		{
			locations.CopyTo(array, arrayIndex);
		}
		#endregion

		#region FindById
		public bool FindById(int id, out TouchLocation touchLocation)
		{
			foreach (var location in locations)
			{
				if (location.Id == id)
				{
					touchLocation = location;
					return true;
				}
			}

			touchLocation = default(TouchLocation);
			return false;
		}
		#endregion

		#region Remove
		public bool Remove(TouchLocation item)
		{
			return locations.Remove(item);
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
