using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
	public sealed class Video
	{
		#region Public
		public TimeSpan Duration
		{
			get;
			private set;
		}
		
		public int Width
		{
			get;
			private set;
		}
		
		public int Height
		{
			get;
			private set;
		}
		
		public float FramesPerSecond
		{
			get;
			private set;
		}
		
		public VideoSoundtrackType VideoSoundtrackType
		{
			get;
			private set;
		}
		#endregion

		#region Constructor
		internal Video(int duration, int width, int height, float framesPerSecond, VideoSoundtrackType soundtrackType)
		{
			Duration = new TimeSpan(0, 0, 0, 0, duration);
			Width = width;
			Height = height;
			FramesPerSecond = framesPerSecond;
			VideoSoundtrackType = soundtrackType;
		}
		#endregion
	}
}
