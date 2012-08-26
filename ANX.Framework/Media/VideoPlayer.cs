using System;
using ANX.Framework.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
	public sealed class VideoPlayer : IDisposable
	{
		public bool IsDisposed
		{
			get;
			private set;
		}

		public TimeSpan PlayPosition
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public float Volume
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool IsMuted
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool IsLooped
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public Video Video
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public MediaState State
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public VideoPlayer()
		{
			IsDisposed = false;
		}

		~VideoPlayer()
		{
			Dispose();
		}

		public void Dispose()
		{
			if (IsDisposed == false)
			{
				IsDisposed = true;
				throw new NotImplementedException();
			}
		}

		public void Play(Video video)
		{
			throw new NotImplementedException();
		}

		public void Pause()
		{
			throw new NotImplementedException();
		}

		public void Resume()
		{
			throw new NotImplementedException();
		}

		public void Stop()
		{
			throw new NotImplementedException();
		}

		public Texture2D GetTexture()
		{
			throw new NotImplementedException();
		}
	}
}
