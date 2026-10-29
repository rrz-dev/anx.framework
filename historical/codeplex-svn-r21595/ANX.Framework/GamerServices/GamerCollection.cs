#region Using Statements
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.GamerServices
{
    [PercentageComplete(10)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public class GamerCollection<T> : ReadOnlyCollection<T>,
		IEnumerable<Gamer>, IEnumerable where T : Gamer
	{
		#region GamerCollectionEnumerator
		public struct GamerCollectionEnumerator
			: IEnumerator<T>, IDisposable, IEnumerator
		{
			private List<T>.Enumerator enumerator;

			public T Current
			{
				get
				{
					return enumerator.Current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return enumerator.Current;
				}
			}

			internal GamerCollectionEnumerator(List<T>.Enumerator setEnumerator)
			{
				enumerator = setEnumerator;
			}

			public void Dispose()
			{
				enumerator.Dispose();
			}

			public bool MoveNext()
			{
				return enumerator.MoveNext();
			}

			void IEnumerator.Reset()
			{
				((IEnumerator)enumerator).Reset();
			}
		}
		#endregion

		public GamerCollection()
			: base(new List<T>())
		{
		}

		#region GetEnumerator
		public new GamerCollection<T>.GamerCollectionEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator<Gamer> IEnumerable<Gamer>.GetEnumerator()
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
