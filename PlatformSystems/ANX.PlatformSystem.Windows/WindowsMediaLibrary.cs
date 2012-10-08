using System;
using System.IO;
using ANX.Framework.Media;
using ANX.Framework.NonXNA.PlatformSystem;
using WMPLib;

namespace ANX.PlatformSystem.Windows
{
    class WindowsMediaLibrary : INativeMediaLibrary
    {
        private WindowsMediaPlayer nativePlayer;

        public WindowsMediaLibrary()
        {
            nativePlayer = new WindowsMediaPlayer();
        }

        public Picture SavePicture(string file, Stream data)
        {
            throw new NotImplementedException();
        }

        public Picture SavePicture(string file, byte[] data)
        {
            throw new NotImplementedException();
        }

        public Picture GetPictureFromToken(string file)
        {
            throw new NotImplementedException();
        }

        public PictureCollection GetPictures()
        {
            throw new NotImplementedException();
        }

        public PictureAlbum GetRootPictureAlbum()
        {
            throw new NotImplementedException();
        }

        public PictureCollection GetSavedPictures()
        {
            throw new NotImplementedException();
        }

        public SongCollection GetSongs()
        {
            var collection = nativePlayer.mediaCollection;
            var list = collection.getAll();
            var songs = new Song[list.count];
            // TODO: sort out songs!
            for (int index = 0; index < songs.Length; index++)
            {
                var newSong = new Song(list.Item[index].name);
                songs[index] = newSong;
            }
            return new SongCollection(songs);
        }

        public ArtistCollection GetArtists()
        {
            throw new NotImplementedException();
        }

        public AlbumCollection GetAlbums()
        {
            throw new NotImplementedException();
        }

        public PlaylistCollection GetPlaylists()
        {
            throw new NotImplementedException();
        }

        public GenreCollection GetGenres()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (nativePlayer != null)
                nativePlayer.close();
            nativePlayer = null;
        }
    }
}
