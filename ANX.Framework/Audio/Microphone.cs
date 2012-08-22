using System;
using System.Collections.ObjectModel;
using ANX.Framework.NonXNA.SoundSystem;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio
{
	public sealed class Microphone
	{
		#region Private
		private static int defaultMicrophone;
		private static ReadOnlyCollection<Microphone> allMicrophones;

		private IMicrophone nativeMicrophone;
		#endregion

		#region Events
		public event EventHandler<EventArgs> BufferReady;
		#endregion

		#region Public
		public static ReadOnlyCollection<Microphone> All
		{
			get
			{
				return allMicrophones;
			}
		}

		public static Microphone Default
		{
			get
			{
				return allMicrophones[defaultMicrophone];
			}
		}

		public readonly string Name;

		public TimeSpan BufferDuration
		{
			get
			{
				return nativeMicrophone.BufferDuration;
			}
			set
			{
				nativeMicrophone.BufferDuration = value;
			}
		}

		public bool IsHeadset
		{
			get
			{
				return nativeMicrophone.IsHeadset;
			}
		}

		public int SampleRate
		{
			get
			{
				return nativeMicrophone.SampleRate;
			}
		}

		public MicrophoneState State
		{
			get
			{
				return nativeMicrophone.State;
			}
		}
		#endregion

		#region Constructor
		static Microphone()
		{
			var creator = AddInSystemFactory.Instance.GetDefaultCreator<ISoundSystemCreator>();
			allMicrophones = creator.GetAllMicrophones();
			defaultMicrophone = creator.GetDefaultMicrophone(allMicrophones);
		}

		internal Microphone(string setName)
		{
			Name = setName;
			var creator = AddInSystemFactory.Instance.GetDefaultCreator<ISoundSystemCreator>();
			nativeMicrophone = creator.CreateMicrophone(this);
		}

		~Microphone()
		{
			if (nativeMicrophone != null)
			{
				nativeMicrophone.Dispose();
				nativeMicrophone = null;
			}
		}
		#endregion

		#region Stop
		public void Stop()
		{
			nativeMicrophone.Stop();
		}
		#endregion

		#region Start
		public void Start()
		{
			nativeMicrophone.Start();
		}
		#endregion

		#region GetSampleSizeInBytes
		public int GetSampleSizeInBytes(TimeSpan duration)
		{
			return nativeMicrophone.GetSampleSizeInBytes(ref duration);
		}
		#endregion

		#region GetSampleDuration
		public TimeSpan GetSampleDuration(int sizeInBytes)
		{
			return nativeMicrophone.GetSampleDuration(sizeInBytes);
		}
		#endregion

		#region GetData
		public int GetData(byte[] buffer)
		{
			return nativeMicrophone.GetData(buffer);
		}
		#endregion

		#region GetData
		public int GetData(byte[] buffer, int offset, int count)
		{
			return nativeMicrophone.GetData(buffer, offset, count);
		}
		#endregion
	}
}
