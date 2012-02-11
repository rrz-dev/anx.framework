using System;
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
	public class SoundEffectInstance : IDisposable
	{
		#region Private
		private SoundEffect parent;

		private ISoundEffectInstance nativeInstance;
		#endregion

		#region Public
		#region IsDisposed
		public bool IsDisposed
		{
			get;
			private set;
		}
		#endregion

		#region IsLooped
		public virtual bool IsLooped
		{
			get
			{
				return nativeInstance.IsLooped;
			}
			set
			{
				nativeInstance.IsLooped = value;
			}
		}
		#endregion

		#region Pan
		public float Pan
		{
			get
			{
				return nativeInstance.Pan;
			}
			set
			{
				nativeInstance.Pan = value;
			}
		}
		#endregion

		#region Pitch
		public float Pitch
		{
			get
			{
				return nativeInstance.Pitch;
			}
			set
			{
				nativeInstance.Pitch = value;
			}
		}
		#endregion

		#region State
		public SoundState State
		{
			get
			{
				return nativeInstance.State;
			}
		}
		#endregion

		#region Volume
		public float Volume
		{
			get
			{
				return nativeInstance.Volume;
			}
			set
			{
				nativeInstance.Volume = value;
			}
		}
		#endregion
		#endregion

		#region Constructor
		protected SoundEffectInstance()
		{
		}

		internal SoundEffectInstance(SoundEffect setParent)
		{
			parent = setParent;

			nativeInstance = GetCreator().CreateSoundEffectInstance(setParent);
		}

		~SoundEffectInstance()
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

		#region Apply3D
		public void Apply3D(AudioListener listener, AudioEmitter emitter)
		{
			Apply3D(new AudioListener[] { listener }, emitter);
		}

		public void Apply3D(AudioListener[] listeners, AudioEmitter emitter)
		{
			nativeInstance.Apply3D(listeners, emitter);
		}
		#endregion

		#region Pause
		public void Pause()
		{
			nativeInstance.Pause();
		}
		#endregion

		#region Play
		public virtual void Play()
		{
			nativeInstance.Play();
		}
		#endregion

		#region Resume
		public void Resume()
		{
			nativeInstance.Resume();
		}
		#endregion

		#region Stop
		public void Stop()
		{
			Stop(true);
		}

		public void Stop(bool immediate)
		{
			nativeInstance.Stop(immediate);
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (nativeInstance != null)
			{
				nativeInstance.Dispose();
				nativeInstance = null;
			}

			IsDisposed = true;
		}
		#endregion
	}
}
