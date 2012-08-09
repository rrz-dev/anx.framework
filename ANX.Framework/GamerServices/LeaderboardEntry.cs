#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.GamerServices
{
	public sealed class LeaderboardEntry
	{
		public Gamer Gamer
		{
			get
			{
				throw new NotSupportedException("Games for Windows LIVE is not supported in ANX");
			}
		}

		public long Rating
		{
			get
			{
				throw new NotSupportedException("Games for Windows LIVE is not supported in ANX");
			}
			set
			{
				throw new NotSupportedException("Games for Windows LIVE is not supported in ANX");
			}
		}

		public PropertyDictionary Columns
		{
			get
			{
				throw new NotSupportedException("Games for Windows LIVE is not supported in ANX");
			}
		}

		internal LeaderboardEntry()
		{
		}
	}
}
