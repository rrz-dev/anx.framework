using System;
using System.IO;
using ANX.Framework.NonXNA.Development;
using ANX.Framework.NonXNA.PlatformSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Untested)]
	public sealed class MediaLibrary : IDisposable
	{
		private INativeMediaLibrary nativeLibrary;

        #region Public
        public bool IsDisposed { get; private set; }
	    public MediaSource MediaSource { get; private set; }

	    public PictureCollection Pictures
	    {
	        get { return nativeLibrary.GetPictures(); }
	    }

	    public PictureAlbum RootPictureAlbum
	    {
	        get { return nativeLibrary.GetRootPictureAlbum(); }
	    }

	    public PictureCollection SavedPictures
	    {
	        get { return nativeLibrary.GetSavedPictures(); }
	    }

	    public SongCollection Songs
	    {
	        get { return nativeLibrary.GetSongs(); }
	    }

	    public ArtistCollection Artists
	    {
	        get { return nativeLibrary.GetArtists(); }
	    }

	    public AlbumCollection Albums
	    {
	        get { return nativeLibrary.GetAlbums(); }
	    }

	    public PlaylistCollection Playlists
	    {
	        get { return nativeLibrary.GetPlaylists(); }
	    }

	    public GenreCollection Genres
	    {
	        get { return nativeLibrary.GetGenres(); }
	    }
	    #endregion

		#region Constructor
		public MediaLibrary()
		{
			nativeLibrary = PlatformSystem.Instance.CreateMediaLibrary();
			MediaSource = MediaSource.GetAvailableMediaSources()[0];
		}

		public MediaLibrary(MediaSource setSource)
		{
			nativeLibrary = PlatformSystem.Instance.CreateMediaLibrary();
			MediaSource = setSource;
		}

		~MediaLibrary()
		{
			Dispose();
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
		    if (IsDisposed)
		        return;

		    IsDisposed = true;
		    nativeLibrary.Dispose();
		    nativeLibrary = null;
		}
		#endregion

		#region SavePicture
		public Picture SavePicture(string file, byte[] data)
		{
			return nativeLibrary.SavePicture(file, data);
		}

		public Picture SavePicture(string file, Stream data)
		{
			return nativeLibrary.SavePicture(file, data);
		}
		#endregion

		#region GetPictureFromToken
		public Picture GetPictureFromToken(string file)
		{
			return nativeLibrary.GetPictureFromToken(file);
		}
		#endregion
	}
}
