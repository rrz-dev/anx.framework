using System;
using ANX.Framework.GamerServices;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Net
{
    [PercentageComplete(0)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
	public sealed class NetworkMachine
	{
		public GamerCollection<NetworkGamer> Gamers
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public void RemoveFromSession()
		{
			throw new NotImplementedException();
		}
	}
}
