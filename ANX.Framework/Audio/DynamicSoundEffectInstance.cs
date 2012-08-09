using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio
{
	public sealed class DynamicSoundEffectInstance : SoundEffectInstance
	{
		#region Events
		public event EventHandler<EventArgs> BufferNeeded;
		#endregion

		#region Public
		public override bool IsLooped
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

		public int PendingBufferCount
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		#endregion

		#region Constructor
		public DynamicSoundEffectInstance(int sampleRate, AudioChannels channels)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetSampleDuration
		public TimeSpan GetSampleDuration(int sizeInBytes)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetSampleSizeInBytes
		public int GetSampleSizeInBytes(TimeSpan duration)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Play
		public override void Play()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region SubmitBuffer
		public void SubmitBuffer(byte[] buffer)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region SubmitBuffer
		public void SubmitBuffer(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Dispose
		protected override void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
