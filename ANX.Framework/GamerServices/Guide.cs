#region Using Statements
using System;
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
    public static class Guide
	{
		public static bool IsTrialMode
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public static bool SimulateTrialMode
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

		public static bool IsVisible
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public static bool IsScreenSaverEnabled
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

		public static NotificationPosition NotificationPosition
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

		public static IAsyncResult BeginShowMessageBox(PlayerIndex player, string title,
			string text, IEnumerable<string> buttons, int focusButton, MessageBoxIcon icon,
			AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		public static IAsyncResult BeginShowMessageBox(string title, string text,
			IEnumerable<string> buttons, int focusButton, MessageBoxIcon icon,
			AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		public static int? EndShowMessageBox(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		public static void ShowSignIn(int paneCount, bool onlineOnly)
		{
			throw new NotImplementedException();
		}

		public static IAsyncResult BeginShowKeyboardInput(PlayerIndex player, string title,
			string description, string defaultText, AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		public static IAsyncResult BeginShowKeyboardInput(PlayerIndex player, string title,
			string description, string defaultText, AsyncCallback callback, object asyncState,
			bool usePasswordMode)
		{
			throw new NotImplementedException();
		}

		public static string EndShowKeyboardInput(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		public static void ShowMessages(PlayerIndex player)
		{
			throw new NotImplementedException();
		}

		public static void ShowFriends(PlayerIndex player)
		{
			throw new NotImplementedException();
		}

		public static void ShowPlayers(PlayerIndex player)
		{
			throw new NotImplementedException();
		}

		public static void ShowFriendRequest(PlayerIndex player, Gamer gamer)
		{
			throw new NotImplementedException();
		}

		public static void ShowPlayerReview(PlayerIndex player, Gamer gamer)
		{
			throw new NotImplementedException();
		}

		public static void ShowGamerCard(PlayerIndex player, Gamer gamer)
		{
			throw new NotImplementedException();
		}

		public static void ShowParty(PlayerIndex player)
		{
			throw new NotImplementedException();
		}

		public static void ShowPartySessions(PlayerIndex player)
		{
			throw new NotImplementedException();
		}

		public static void ShowComposeMessage(PlayerIndex player, string text,
			IEnumerable<Gamer> recipients)
		{
			throw new NotImplementedException();
		}

		public static void DelayNotifications(TimeSpan delay)
		{
			throw new NotImplementedException();
		}

		public static void ShowGameInvite(PlayerIndex player, IEnumerable<Gamer> recipients)
		{
			throw new NotImplementedException();
		}

		public static void ShowGameInvite(string sessionId)
		{
			throw new NotImplementedException();
		}

		public static void ShowMarketplace(PlayerIndex player)
		{
			throw new NotImplementedException();
		}
	}
}
