#region Using Statements
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed class LeaderboardReader : IDisposable
	{
		public LeaderboardIdentity LeaderboardIdentity
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int TotalLeaderboardSize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int PageStart
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool IsDisposed
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool CanPageUp
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool CanPageDown
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public ReadOnlyCollection<LeaderboardEntry> Entries
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public static LeaderboardReader Read(LeaderboardIdentity identity,
			IEnumerable<Gamer> gamers, Gamer pivotGamer, int pageSize)
		{
			throw new NotImplementedException();
		}

		public static LeaderboardReader Read(LeaderboardIdentity identity,
			Gamer pivotGamer, int pageSize)
		{
			throw new NotImplementedException();
		}

		public static LeaderboardReader Read(LeaderboardIdentity identity,
			int pageStart, int pageSize)
		{
			throw new NotImplementedException();
		}

		public static IAsyncResult BeginRead(LeaderboardIdentity identity,
			IEnumerable<Gamer> gamers, Gamer pivotGamer, int pageSize,
			AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		public static IAsyncResult BeginRead(LeaderboardIdentity identity,
			Gamer pivotGamer, int pageSize, AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		public static IAsyncResult BeginRead(LeaderboardIdentity identity,
			int pageStart, int pageSize, AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		public static LeaderboardReader EndRead(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		public void PageUp()
		{
			throw new NotImplementedException();
		}

		public void PageDown()
		{
			throw new NotImplementedException();
		}

		public IAsyncResult BeginPageUp(AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		public IAsyncResult BeginPageDown(AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		public void EndPageUp(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		public void EndPageDown(IAsyncResult result)
		{
			throw new NotImplementedException();
		}
	}
}
