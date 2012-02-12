using System;
using System.IO;
using ANX.Framework.Audio;
using ANX.Framework.NonXNA.SoundSystem;

namespace ANX.SoundSystem.OpenAL
{
	public class OpenALSoundEffect : ISoundEffect
	{
		#region Private
		internal SoundEffect parent;
		#endregion

		#region Public (TODO)
		public TimeSpan Duration
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		#endregion

		#region Constructor
		internal OpenALSoundEffect(SoundEffect setParent, Stream stream)
		{
			parent = setParent;
		}
		#endregion

		#region Dispose (TODO)
		public void Dispose()
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
