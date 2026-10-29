#region Using Statements
using System;
using ANX.Framework.Audio;
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
    public sealed class SignedInGamer : Gamer
	{
		public PlayerIndex PlayerIndex
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		
		public bool IsSignedInToLive
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		
		public bool IsGuest
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		
		public GameDefaults GameDefaults
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		
		public GamerPrivileges Privileges
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		
		public int PartySize
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		
		public GamerPresence Presence
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public event EventHandler<SignedInEventArgs> SignedIn;
		public event EventHandler<SignedOutEventArgs> SignedOut;

		public bool IsFriend(Gamer gamer)
		{
			throw new NotImplementedException();
		}

		public FriendCollection GetFriends()
		{
			throw new NotImplementedException();
		}

		public bool IsHeadset(Microphone microphone)
		{
			throw new NotImplementedException();
		}

		public IAsyncResult BeginAwardAchievement(string achievementKey, AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		public void EndAwardAchievement(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		public void AwardAchievement(string achievementKey)
		{
			throw new NotImplementedException();
		}

		public IAsyncResult BeginGetAchievements(AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		public AchievementCollection EndGetAchievements(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		public AchievementCollection GetAchievements()
		{
			throw new NotImplementedException();
		}
	}
}
