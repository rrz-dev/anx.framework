using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
	public static class MediaPlayer
	{
		#region Events
		public static event EventHandler<EventArgs> ActiveSongChanged;
		public static event EventHandler<EventArgs> MediaStateChanged;
		#endregion

		#region Public
		#region IsShuffled (TODO)
		public static bool IsShuffled
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
		#endregion

		#region IsRepeating (TODO)
		public static bool IsRepeating
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
		#endregion

		#region Volume (TODO)
		public static float Volume
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
		#endregion

		#region IsMuted (TODO)
		public static bool IsMuted
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
		#endregion

		#region IsVisualizationEnabled (TODO)
		public static bool IsVisualizationEnabled
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
		#endregion

		#region Queue (TODO)
		public static MediaQueue Queue
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		#endregion

		#region State (TODO)
		public static MediaState State
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		#endregion

		#region PlayPosition (TODO)
		public static TimeSpan PlayPosition
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		#endregion

		#region GameHasControl (TODO)
		public static bool GameHasControl
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		#endregion
		#endregion

		#region Constructor
		static MediaPlayer()
		{
		}
		#endregion

		#region Play (TODO)
		public static void Play(Song song)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Play (TODO)
		public static void Play(SongCollection songCollection)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Play (TODO)
		public static void Play(SongCollection songCollection, int index)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Pause (TODO)
		public static void Pause()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Resume (TODO)
		public static void Resume()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Stop (TODO)
		public static void Stop()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region MoveNext (TODO)
		public static void MoveNext()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region MovePrevious (TODO)
		public static void MovePrevious()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetVisualizationData (TODO)
		public static void GetVisualizationData(VisualizationData data)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
