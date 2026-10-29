#region Using Statements
using System;
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
    public abstract class Gamer
    {
        public static IAsyncResult BeginGetFromGamertag(string gamertag, AsyncCallback callback, Object asyncState)
        {
            throw new NotImplementedException();
        }

        public static IAsyncResult BeginGetPartnerToken(string audienceUri, AsyncCallback callback, Object asyncState)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginGetProfile(AsyncCallback callback, Object asyncState)
        {
            throw new NotImplementedException();
        }

        public static Gamer EndGetFromGamertag(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public static string EndGetPartnerToken(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public GamerProfile EndGetProfile(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public static Gamer GetFromGamertag(string gamertag)
        {
            throw new NotImplementedException();
        }

        public static string GetPartnerToken(string audienceUri)
        {
            throw new NotImplementedException();
        }

        public GamerProfile GetProfile()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public string DisplayName
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

        public string Gamertag
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

        public LeaderboardWriter LeaderboardWriter
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public static SignedInGamerCollection SignedInGamers
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Object Tag
        {
            get;
            set;
        }
    }
}
