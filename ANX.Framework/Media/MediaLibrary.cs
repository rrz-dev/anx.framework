using System;
using System.IO;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
	public sealed class MediaLibrary : IDisposable
	{
		#region Public
		public PictureCollection Pictures
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		
		public PictureAlbum RootPictureAlbum
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		
		public PictureCollection SavedPictures
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		
		public MediaSource MediaSource
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		
		public SongCollection Songs
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		
		public ArtistCollection Artists
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		
		public AlbumCollection Albums
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		
		public PlaylistCollection Playlists
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		
		public GenreCollection Genres
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		
		public bool IsDisposed
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		#endregion

		#region Constructor
		public MediaLibrary()
		{
			throw new NotImplementedException();
		}

		public MediaLibrary(MediaSource setSource)
		{
			throw new NotImplementedException();
		}

		~MediaLibrary()
		{
			Dispose();
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region SavePicture
		public Picture SavePicture(string file, byte[] data)
		{
			throw new NotImplementedException();
		}

		public Picture SavePicture(string file, Stream data)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetPictureFromToken
		public Picture GetPictureFromToken(string file)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
