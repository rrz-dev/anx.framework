using System;
using System.Collections.Generic;
using System.IO;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.SoundSystem;

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

namespace ANX.Framework.Audio
{
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
		internal ISoundEffect nativeSoundEffect;

		private static List<SoundEffectInstance> fireAndForgetInstances;

		private List<WeakReference> children = new List<WeakReference>();
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
			MasterVolume = 1f;
			SpeedOfSound = 343.5f;
			DopplerScale = 1f;
			DistanceScale = 1f;
		}

		private SoundEffect(Stream stream)
		{
			nativeSoundEffect = GetCreator().CreateSoundEffect(this, stream);
		}

		public SoundEffect(byte[] buffer, int sampleRate, AudioChannels channels)
			 : this(buffer, 0, buffer.Length, sampleRate, channels, 0, 0)
		{
		}

		public SoundEffect(byte[] buffer, int offset, int count, int sampleRate,
			AudioChannels channels, int loopStart, int loopLength)
		{
			nativeSoundEffect = GetCreator().CreateSoundEffect(this, buffer, offset,
				count, sampleRate, channels, loopStart, loopLength);
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
		public static TimeSpan GetSampleDuration(int sizeInBytes, int sampleRate,
			AudioChannels channels)
		{
			float sizeMulBlockAlign = sizeInBytes / ((int)channels * 2);
			return TimeSpan.FromMilliseconds((double)(sizeMulBlockAlign * 1000f /
				(float)sampleRate));
		}
		#endregion

		#region GetSampleSizeInBytes
		public static int GetSampleSizeInBytes(TimeSpan duration, int sampleRate,
			AudioChannels channels)
		{
			int timeMulSamples = (int)(duration.TotalMilliseconds *
				(double)((float)sampleRate / 1000f));
			return (timeMulSamples + timeMulSamples % (int)channels) *
				((int)channels * 2);
		}
		#endregion

		#region Play
		public bool Play()
		{
			return Play(1f, 0f, 0f);
		}

		public bool Play(float volume, float pitch, float pan)
		{
			if (IsDisposed)
			{
				return false;
			}

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
			catch
			{
				return false;
			}

			return true;
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			if (IsDisposed)
			{
				return;
			}

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
