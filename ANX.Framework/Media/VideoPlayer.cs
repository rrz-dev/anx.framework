using System;
using ANX.Framework.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
	public sealed class VideoPlayer : IDisposable
	{
	    private float volume;
        internal float VolumeToUse
        {
            get { return IsMuted ? 0f : volume; }
        }

        public Video Video { get; private set; }
        public bool IsMuted { get; set; }
	    public bool IsDisposed { get; private set; }

	    public TimeSpan PlayPosition
		{
			get { return Video == null ? TimeSpan.Zero : Video.PlayPosition; }
		}

		public float Volume
		{
			get { return volume; }
            set
            {
                if (value < 0f || value > 1f)
                    throw new ArgumentOutOfRangeException("value");

                volume = value;
            }
		}

		public bool IsLooped
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
		    IsMuted = false;
		    volume = 1f;
			IsDisposed = false;
		}

		~VideoPlayer()
		{
			Dispose();
		}

		public void Dispose()
		{
		    if (IsDisposed)
		        return;

		    IsDisposed = true;
		    Video = null;
		}

		public void Play(Video video)
        {
            if (video == null)
                throw new ArgumentNullException("video");

		    Video = video;
		    video.Play();
		}

		public void Pause()
        {
            if (Video != null)
                Video.Pause();
		}

		public void Resume()
        {
            if (Video != null)
                Video.Resume();
		}

		public void Stop()
        {
            if (Video != null)
                Video.Stop();
		}

		public Texture2D GetTexture()
		{
			throw new NotImplementedException();
		}
	}
}
