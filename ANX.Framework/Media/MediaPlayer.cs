using System;

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
