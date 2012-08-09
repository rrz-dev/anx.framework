using System;
using System.Collections.ObjectModel;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio
{
	public sealed class Microphone
	{
		#region Events
		public event EventHandler<EventArgs> BufferReady;
		#endregion

		#region Public
		public readonly string Name;

		public static ReadOnlyCollection<Microphone> All
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public TimeSpan BufferDuration
		{
			get;
			set;
		}

		public static Microphone Default
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool IsHeadset
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int SampleRate
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public MicrophoneState State
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		#endregion

		#region Constructor
		~Microphone()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Stop
		public void Stop()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Start
		public void Start()
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

		#region GetSampleDuration
		public TimeSpan GetSampleDuration(int sizeInBytes)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetData
		public int GetData(byte[] buffer)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetData
		public int GetData(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
