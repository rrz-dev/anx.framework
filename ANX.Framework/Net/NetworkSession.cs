using System;
using ANX.Framework.GamerServices;
using System.Collections.Generic;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Net
{
    [PercentageComplete(0)]
    [TestState(TestStateAttribute.TestState.Untested)]
	public sealed class NetworkSession : IDisposable
	{
		public const int MaxSupportedGamers = 31;
		public const int MaxPreviousGamers = 100;

		public event EventHandler<NetworkSessionEndedEventArgs> SessionEnded;
		public event EventHandler<GamerJoinedEventArgs> GamerJoined;
		public event EventHandler<GamerLeftEventArgs> GamerLeft;
		public event EventHandler<GameStartedEventArgs> GameStarted;
		public event EventHandler<GameEndedEventArgs> GameEnded;
		public event EventHandler<HostChangedEventArgs> HostChanged;
		public event EventHandler<InviteAcceptedEventArgs> InviteAccepted;
		public event EventHandler<WriteLeaderboardsEventArgs> WriteArbitratedLeaderboard;
		public event EventHandler<WriteLeaderboardsEventArgs> WriteTrueSkill;
		public event EventHandler<WriteLeaderboardsEventArgs> WriteUnarbitratedLeaderboard;

		public bool IsDisposed
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool AllowJoinInProgress
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

		public bool AllowHostMigration
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

		public bool IsEveryoneReady
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool IsHost
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public NetworkSessionType SessionType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public NetworkSessionState SessionState
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int BytesPerSecondSent
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int BytesPerSecondReceived
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public NetworkSessionProperties SessionProperties
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public TimeSpan SimulatedLatency
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

		public float SimulatedPacketLoss
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

		public int PrivateGamerSlots
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

		public int MaxGamers
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

		public NetworkGamer Host
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public GamerCollection<NetworkGamer> AllGamers
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public GamerCollection<LocalNetworkGamer> LocalGamers
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public GamerCollection<NetworkGamer> RemoteGamers
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public GamerCollection<NetworkGamer> PreviousGamers
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		~NetworkSession()
		{
			Dispose();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public void Update()
		{
			throw new NotImplementedException();
		}

		public void StartGame()
		{
			throw new NotImplementedException();
		}

		public void EndGame()
		{
			throw new NotImplementedException();
		}

		public void ResetReady()
		{
			throw new NotImplementedException();
		}

		public void AddLocalGamer(SignedInGamer gamer) 
		{
			throw new NotImplementedException();
		}

		public NetworkGamer FindGamerById(byte gamerId)
		{
			throw new NotImplementedException();
		}

		public static NetworkSession EndJoinInvited(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		public static NetworkSession Create(NetworkSessionType sessionType, int maxLocalGamers, int maxGamers)
		{
			throw new NotImplementedException();
		}

		public static NetworkSession Create(NetworkSessionType sessionType, int maxLocalGamers, int maxGamers,
            int privateGamerSlots, NetworkSessionProperties sessionProperties)
		{
			throw new NotImplementedException();
		}

		public static NetworkSession Create(NetworkSessionType sessionType, IEnumerable<SignedInGamer> localGamers,
            int maxGamers, int privateGamerSlots, NetworkSessionProperties sessionProperties)
		{
			throw new NotImplementedException();
		}

		public static IAsyncResult BeginCreate(NetworkSessionType sessionType, int maxLocalGamers, int maxGamers,
            AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		public static IAsyncResult BeginCreate(NetworkSessionType sessionType, int maxLocalGamers, int maxGamers,
            int privateGamerSlots, NetworkSessionProperties sessionProperties, AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		public static IAsyncResult BeginCreate(NetworkSessionType sessionType, IEnumerable<SignedInGamer> localGamers,
            int maxGamers, int privateGamerSlots, NetworkSessionProperties sessionProperties, AsyncCallback callback,
			object asyncState)
		{
			throw new NotImplementedException();
		}

		public static NetworkSession EndCreate(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		public static AvailableNetworkSessionCollection Find(NetworkSessionType sessionType, int maxLocalGamers,
            NetworkSessionProperties searchProperties)
		{
			throw new NotImplementedException();
		}

		public static AvailableNetworkSessionCollection Find(NetworkSessionType sessionType,
            IEnumerable<SignedInGamer> localGamers, NetworkSessionProperties searchProperties)
		{
			throw new NotImplementedException();
		}

		public static IAsyncResult BeginFind(NetworkSessionType sessionType, int maxLocalGamers,
            NetworkSessionProperties searchProperties, AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		public static IAsyncResult BeginFind(NetworkSessionType sessionType, IEnumerable<SignedInGamer> localGamers,
            NetworkSessionProperties searchProperties, AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		public static AvailableNetworkSessionCollection EndFind(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		public static NetworkSession Join(AvailableNetworkSession availableSession)
		{
			throw new NotImplementedException();
		}

		public static IAsyncResult BeginJoin(AvailableNetworkSession availableSession, AsyncCallback callback,
            object asyncState)
		{
			throw new NotImplementedException();
		}

		public static NetworkSession EndJoin(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		public static NetworkSession JoinInvited(int maxLocalGamers)
		{
			throw new NotImplementedException();
		}

		public static NetworkSession JoinInvited(IEnumerable<SignedInGamer> localGamers)
		{
			throw new NotImplementedException();
		}

		public static IAsyncResult BeginJoinInvited(int maxLocalGamers, AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		public static IAsyncResult BeginJoinInvited(IEnumerable<SignedInGamer> localGamers, AsyncCallback callback,
            object asyncState)
		{
			throw new NotImplementedException();
		}
	}
}
