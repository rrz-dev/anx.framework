using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Net
{
	public sealed class AvailableNetworkSessionCollection :
		ReadOnlyCollection<AvailableNetworkSession>, IDisposable
	{
		public bool IsDisposed
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		internal AvailableNetworkSessionCollection(IList<AvailableNetworkSession> sessions)
			: base(sessions)
		{
		}

		~AvailableNetworkSessionCollection()
		{
			Dispose();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
