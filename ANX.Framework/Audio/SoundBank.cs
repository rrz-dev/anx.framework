using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio
{
    [PercentageComplete(0)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Untested)]
	public class SoundBank : IDisposable
	{
		#region Events
		public event EventHandler<EventArgs> Disposing;
		#endregion
		
		#region Public
        public bool IsDisposed { get; private set; }

        public bool IsInUse
        {
            get { throw new NotImplementedException(); }
        }
        #endregion
		
		#region Constructor
		public SoundBank(AudioEngine audioEngine, string filename)
		{
			throw new NotImplementedException();
		}

		~SoundBank()
		{
			Dispose();
		}
		#endregion

		#region GetCue
		public Cue GetCue(string name)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region PlayCue
		public void PlayCue(string name)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region PlayCue
		public void PlayCue(string name, AudioListener listener, AudioEmitter emitter)
		{
			throw new NotImplementedException();
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
				return;

			IsDisposed = true;
			throw new NotImplementedException();
		}
		#endregion
	}
}
