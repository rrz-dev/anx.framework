using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio
{
	public class WaveBank : IDisposable
	{
		#region Events
		public event EventHandler<EventArgs> Disposing;
		#endregion

		#region Public
		public bool IsDisposed
		{
			get;
			private set;
		}

		public bool IsInUse
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool IsPrepared
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		#endregion

		#region Constructor
		public WaveBank(AudioEngine audioEngine, string nonStreamingWaveBankFilename)
		{

		}
		public WaveBank(AudioEngine audioEngine, string streamingWaveBankFilename,
			int offset, short packetsize)
		{

		}

		~WaveBank()
		{
			Dispose();
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (IsDisposed)
			{
				return;
			}

			IsDisposed = true;

			throw new NotImplementedException();
		}
		#endregion
	}
}
