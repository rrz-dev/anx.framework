using System;
using ANX.Framework.Storage;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.PlatformSystem
{
	public interface INativeStorageDevice
	{
		long FreeSpace
		{
			get;
		}

		long TotalSpace
		{
			get;
		}

		bool IsConnected
		{
			get;
		}

		void DeleteContainer(string titleName);
	}
}
