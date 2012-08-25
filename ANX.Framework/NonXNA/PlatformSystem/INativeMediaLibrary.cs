using System;
using System.IO;
using ANX.Framework.Media;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.PlatformSystem
{
	public interface INativeMediaLibrary : IDisposable
	{
		Picture SavePicture(string file, Stream data);
		Picture SavePicture(string file, byte[] data);
		Picture GetPictureFromToken(string file);

		PictureCollection GetPictures();
		PictureAlbum GetRootPictureAlbum();
		PictureCollection GetSavedPictures();
		SongCollection GetSongs();
		ArtistCollection GetArtists();
		AlbumCollection GetAlbums();
		PlaylistCollection GetPlaylists();
		GenreCollection GetGenres();
	}
}
