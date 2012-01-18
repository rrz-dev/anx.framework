using System;
using ANX.Framework.GamerServices;
using System.Collections.Generic;

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

namespace ANX.Framework.Net
{
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

		public static NetworkSession Create(NetworkSessionType sessionType,
			int maxLocalGamers, int maxGamers)
		{
			throw new NotImplementedException();
		}

		public static NetworkSession Create(NetworkSessionType sessionType,
			int maxLocalGamers, int maxGamers, int privateGamerSlots,
			NetworkSessionProperties sessionProperties)
		{
			throw new NotImplementedException();
		}

		public static NetworkSession Create(NetworkSessionType sessionType,
			IEnumerable<SignedInGamer> localGamers, int maxGamers, int privateGamerSlots,
			NetworkSessionProperties sessionProperties)
		{
			throw new NotImplementedException();
		}

		public static IAsyncResult BeginCreate(NetworkSessionType sessionType,
			int maxLocalGamers, int maxGamers, AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		public static IAsyncResult BeginCreate(NetworkSessionType sessionType,
			int maxLocalGamers, int maxGamers, int privateGamerSlots,
			NetworkSessionProperties sessionProperties, AsyncCallback callback,
			object asyncState)
		{
			throw new NotImplementedException();
		}

		public static IAsyncResult BeginCreate(NetworkSessionType sessionType,
			IEnumerable<SignedInGamer> localGamers, int maxGamers, int privateGamerSlots,
			NetworkSessionProperties sessionProperties, AsyncCallback callback,
			object asyncState)
		{
			throw new NotImplementedException();
		}

		public static NetworkSession EndCreate(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		public static AvailableNetworkSessionCollection Find(NetworkSessionType sessionType,
			int maxLocalGamers, NetworkSessionProperties searchProperties)
		{
			throw new NotImplementedException();
		}

		public static AvailableNetworkSessionCollection Find(NetworkSessionType sessionType,
			IEnumerable<SignedInGamer> localGamers, NetworkSessionProperties searchProperties)
		{
			throw new NotImplementedException();
		}

		public static IAsyncResult BeginFind(NetworkSessionType sessionType,
			int maxLocalGamers, NetworkSessionProperties searchProperties,
			AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		public static IAsyncResult BeginFind(NetworkSessionType sessionType,
			IEnumerable<SignedInGamer> localGamers, NetworkSessionProperties searchProperties,
			AsyncCallback callback, object asyncState)
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

		public static IAsyncResult BeginJoin(AvailableNetworkSession availableSession,
			AsyncCallback callback, object asyncState)
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

		public static IAsyncResult BeginJoinInvited(int maxLocalGamers,
			AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		public static IAsyncResult BeginJoinInvited(IEnumerable<SignedInGamer> localGamers,
			AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}
	}
}
