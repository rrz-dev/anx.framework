using System;
using ANX.Framework.NonXNA.PlatformSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.Metro
{
	public class MetroStorageDevice : INativeStorageDevice
	{
		public long FreeSpace
		{
			get
			{
				return 0;
			}
		}

		public long TotalSpace
		{
			get
			{
				return 0;
			}
		}

		public bool IsConnected
		{
			get
			{
				return true;
			}
		}

		public void DeleteContainer(string titleName)
		{
			throw new NotImplementedException();
		}
	}
}
