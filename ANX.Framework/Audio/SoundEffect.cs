using System;
using System.Collections.Generic;
using System.IO;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;
using ANX.Framework.NonXNA.SoundSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.InProgress)]
	[Developer("AstrorEnales")]
	public sealed class SoundEffect : IDisposable
	{
		#region Static
		#region DistanceScale
		public static float DistanceScale
		{
			get
			{
				return GetCreator().DistanceScale;
			}
			set
			{
				GetCreator().DistanceScale = value;
			}
		}
		#endregion

		#region DopplerScale
		public static float DopplerScale
		{
			get
			{
				return GetCreator().DopplerScale;
			}
			set
			{
				GetCreator().DopplerScale = value;
			}
		}
		#endregion

		#region MasterVolume
		public static float MasterVolume
		{
			get
			{
				return GetCreator().MasterVolume;
			}
			set
			{
				GetCreator().MasterVolume = value;
			}
		}
		#endregion

		#region SpeedOfSound
		public static float SpeedOfSound
		{
			get
			{
				return GetCreator().SpeedOfSound;
			}
			set
			{
				GetCreator().SpeedOfSound = value;
			}
		}
		#endregion
		#endregion

		#region Private
		private static List<SoundEffectInstance> fireAndForgetInstances;
		private List<WeakReference> children;
		internal ISoundEffect nativeSoundEffect;
		#endregion

		#region Public
		public TimeSpan Duration
		{
			get
			{
				return nativeSoundEffect.Duration;
			}
		}

		public bool IsDisposed
		{
			get;
			private set;
		}

		public string Name
		{
			get;
			set;
		}
		#endregion

		#region Constructor
		static SoundEffect()
		{
			fireAndForgetInstances = new List<SoundEffectInstance>();

			MasterVolume = 1f;
			SpeedOfSound = 343.5f;
			DopplerScale = 1f;
			DistanceScale = 1f;
		}

		private SoundEffect()
		{
			children = new List<WeakReference>();
			IsDisposed = false;
		}

		private SoundEffect(Stream stream)
			: this()
		{
			var creator = GetCreator();
			nativeSoundEffect = creator.CreateSoundEffect(this, stream);
		}

		public SoundEffect(byte[] buffer, int sampleRate, AudioChannels channels)
			 : this(buffer, 0, buffer.Length, sampleRate, channels, 0, 0)
		{
		}

		public SoundEffect(byte[] buffer, int offset, int count, int sampleRate, AudioChannels channels, int loopStart, int loopLength)
			: this()
		{
			var creator = GetCreator();
			nativeSoundEffect = creator.CreateSoundEffect(this, buffer, offset, count, sampleRate, channels, loopStart, loopLength);
		}

		~SoundEffect()
		{
			Dispose();
		}
		#endregion

		#region GetCreator
		private static ISoundSystemCreator GetCreator()
		{
			return AddInSystemFactory.Instance.GetDefaultCreator<ISoundSystemCreator>();
		}
		#endregion

		#region CreateInstance
		public SoundEffectInstance CreateInstance()
		{
			return new SoundEffectInstance(this, false);
		}
		#endregion

		#region FromStream
		public static SoundEffect FromStream(Stream stream)
		{
			return new SoundEffect(stream);
		}
		#endregion

		#region GetSampleDuration
		public static TimeSpan GetSampleDuration(int sizeInBytes, int sampleRate, AudioChannels channels)
		{
			float sizeMulBlockAlign = sizeInBytes / ((int)channels * 2);
			return TimeSpan.FromMilliseconds((double)(sizeMulBlockAlign * 1000f / (float)sampleRate));
		}
		#endregion

		#region GetSampleSizeInBytes
		public static int GetSampleSizeInBytes(TimeSpan duration, int sampleRate,
			AudioChannels channels)
		{
			int timeMulSamples = (int)(duration.TotalMilliseconds * (double)((float)sampleRate / 1000f));
			return (timeMulSamples + timeMulSamples % (int)channels) * ((int)channels * 2);
		}
		#endregion

		#region Play
		public bool Play()
		{
			return Play(1f, 1f, 0f);
		}

		public bool Play(float volume, float pitch, float pan)
		{
			if (IsDisposed)
				return false;

			try
			{
				SoundEffectInstance newInstance = new SoundEffectInstance(this, true)
				{
					Volume = volume,
					Pitch = pitch,
					Pan = pan,
				};

				children.Add(new WeakReference(newInstance));

				lock (fireAndForgetInstances)
				{
					fireAndForgetInstances.Add(newInstance);
				}

				newInstance.Play();
			}
			catch (Exception ex)
			{
				Logger.Warning("Failed to play sound effect cause of: " + ex);
				return false;
			}

			return true;
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			if (IsDisposed)
				return;

			IsDisposed = true;
			nativeSoundEffect.Dispose();
			nativeSoundEffect = null;

			List<WeakReference> weakRefs = new List<WeakReference>(children);

			lock (fireAndForgetInstances)
			{
				foreach (WeakReference current in weakRefs)
				{
					SoundEffectInstance soundInstance =
						current.Target as SoundEffectInstance;

					if (soundInstance != null)
					{
						if (soundInstance.IsFireAndForget)
						{
							fireAndForgetInstances.Remove(soundInstance);
						}
						soundInstance.Dispose();
					}
				}
			}

			weakRefs.Clear();
			children.Clear();
		}
		#endregion
	}
}
