#region Using Statements
using System;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.GamerServices
{
    [PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public sealed class SignedInGamerCollection : GamerCollection<SignedInGamer>
	{
		public SignedInGamer this[PlayerIndex playerIndex]
		{
			get
			{
				int count = base.Count;
				for (int index = 0; index < count; index++)
				{
					SignedInGamer signedInGamer = base[index];
					if (signedInGamer.PlayerIndex == playerIndex)
					{
						return signedInGamer;
					}
				}
				return null;
			}
		}
	}
}
