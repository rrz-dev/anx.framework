using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ANX.Framework.Media;
using ANX.Framework.NonXNA.PlatformSystem;
using WMPLib;

namespace ANX.PlatformSystem.Windows
{
    /// <summary>
    /// List of media item attributes: http://msdn.microsoft.com/en-us/library/dd562379%28v=vs.85%29.aspx
    /// </summary>
    public class WindowsMediaLibrary : INativeMediaLibrary
    {
        private enum MediaType
        {
            Audio,
            Other,
            Photo,
            Playlist,
            Radio,
            Video
        }

        private PictureCollection pictureCollection;
        private PictureAlbum rootPictureAlbum;
        private PictureCollection savedPictures;
        private SongCollection songs;
        private ArtistCollection artists;
        private GenreCollection genres;
        private PlaylistCollection playlists;
        private AlbumCollection albums;
        private WindowsMediaPlayer nativePlayer;

        public WindowsMediaLibrary()
        {
            nativePlayer = new WindowsMediaPlayer();
            CollectAllData();
        }

        #region CollectAllData
        private void CollectAllData()
        {
            var allArtists = new List<Artist>();
            var allSongs = new List<Song>();
            var allAlbums = new List<Album>();
            var allPictures = new List<Picture>();
            var allGenres = new List<Genre>();
            var allPictureAlbums = new List<PictureAlbum>();

            var list = nativePlayer.mediaCollection.getAll();
            for (int index = 0; index < list.count; index++)
            {
                var media = list.Item[index];
                string id = media.getItemInfo("TrackingID");
                if (GetType(media) == MediaType.Audio)
                {
                    string artistName = media.getItemInfo("Author");
                    var newArtist = GetArtist(artistName, allArtists);

                    string albumId = media.getItemInfo("AlbumID");
                    string albumName = media.getItemInfo("WM/AlbumTitle");
                    string albumArtistName = albumName.Length > 0 ? albumId.Replace(albumName, "") : artistName;
                    if (albumArtistName.Length == 0)
                        albumArtistName = artistName;
                    var newAlbum = GetAlbum(albumId, allAlbums);
                    newAlbum.Name = albumName;
                    newAlbum.Artist = GetArtist(albumArtistName, allArtists);

                    string genreName = media.getItemInfo("WM/Genre");
                    Genre newGenre = GetGenre(genreName, allGenres);

                    int duration = GetMediaDuration(media, "Duration");
                    string name = media.getItemInfo("Title");
                    var newSong = new Song(name, id, media.sourceURL, duration)
                    {
                        Artist = newArtist,
                        Album = newAlbum,
                        Genre = newGenre,
                        Rating = GetMediaIntValue(media, "UserRating", 0),
                        TrackNumber = GetMediaIntValue(media, "WM/TrackNumber", 0),
                        PlayCount = GetMediaIntValue(media, "UserPlayCount", 0),
                        IsProtected = GetMediaBoolValue(media, "Is_Protected", false),
                    };
                    allSongs.Add(newSong);
                }
                else if (GetType(media) == MediaType.Photo)
                {
                    string name = media.getItemInfo("Title");
                    var newPicture = new Picture(id)
                    {
                        Name = name,
                    };
                    allPictures.Add(newPicture);
                }
            }

            foreach (Artist artist in allArtists)
            {
                var allSongsFromArtist = allSongs.Where(song => song.Artist == artist).ToArray();
                artist.Songs = new SongCollection(allSongsFromArtist);
                var allAlbumsFromArtist = allAlbums.Where(album => album.Artist == artist).ToArray();
                artist.Albums = new AlbumCollection(allAlbumsFromArtist);
            }

            foreach (Album album in allAlbums)
            {
                var allSongsFromAlbum = allSongs.Where(song => song.Album == album).ToArray();
                album.Songs = new SongCollection(allSongsFromAlbum);
            }

            foreach (Genre genre in allGenres)
            {
                var allSongsInGenre = allSongs.Where(song => song.Genre == genre).ToArray();
                genre.Songs = new SongCollection(allSongsInGenre);
                // TODO: albums
            }

            songs = new SongCollection(allSongs);
            pictureCollection = new PictureCollection(allPictures);
            artists = new ArtistCollection(allArtists);
            albums = new AlbumCollection(allAlbums);
            genres = new GenreCollection(allGenres);
            // TODO
            rootPictureAlbum = null;
            playlists = new PlaylistCollection();
            savedPictures = new PictureCollection(new Picture[0]);
        }
        #endregion

        private Artist GetArtist(string name, List<Artist> allArtists)
        {
            foreach (Artist artist in allArtists)
                if (artist.Name == name)
                    return artist;

            var newArtist = new Artist(name);
            allArtists.Add(newArtist);
            return newArtist;
        }

        private Genre GetGenre(string name, List<Genre> allGenres)
        {
            foreach (Genre genre in allGenres)
                if (genre.Name == name)
                    return genre;

            var newGenre = new Genre(name);
            allGenres.Add(newGenre);
            return newGenre;
        }

        private Album GetAlbum(string id, List<Album> allAlbums)
        {
            foreach (Album album in allAlbums)
                if (album.Name == id)
                    return album;

            var newAlbum = new Album(id);
            allAlbums.Add(newAlbum);
            return newAlbum;
        }

        #region GetMethods
        public PictureCollection GetPictures()
        {
            return pictureCollection;
        }

        public PictureAlbum GetRootPictureAlbum()
        {
            return rootPictureAlbum;
        }

        public PictureCollection GetSavedPictures()
        {
            return savedPictures;
        }

        public SongCollection GetSongs()
        {
            return songs;
        }

        public ArtistCollection GetArtists()
        {
            return artists;
        }

        public AlbumCollection GetAlbums()
        {
            return albums;
        }

        public PlaylistCollection GetPlaylists()
        {
            return playlists;
        }

        public GenreCollection GetGenres()
        {
            return genres;
        }
        #endregion

        #region GetMediaBoolValue
        private bool GetMediaBoolValue(IWMPMedia media, string name, bool defaultValue)
        {
            string value = media.getItemInfo(name);
            bool result;
            return bool.TryParse(value, out result) == false ? defaultValue : result;
        }
        #endregion

        #region GetMediaDuration
        private int GetMediaDuration(IWMPMedia media, string name)
        {
            string value = media.getItemInfo(name).Replace(',', '.');
            float seconds;
            if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out seconds) == false)
                return 0;
            return (int)(seconds * 1000f);
        }
        #endregion

        #region GetMediaIntValue
        private int GetMediaIntValue(IWMPMedia media, string name, int defaultValue)
        {
            string value = media.getItemInfo(name);
            int result;
            return int.TryParse(value, out result) == false ? defaultValue : result;
        }
        #endregion
        
        #region GetType
        private static MediaType GetType(IWMPMedia media)
        {
            switch (media.getItemInfo("MediaType"))
            {
                case "audio":
                    return MediaType.Audio;
                default:
                    return MediaType.Other;
                case "photo":
                    return MediaType.Photo;
                case "playlist":
                    return MediaType.Playlist;
                case "radio":
                    return MediaType.Radio;
                case "video":
                    return MediaType.Video;
            }
        }
        #endregion

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

        #region Dispose
        public void Dispose()
        {
            pictureCollection = null;
            rootPictureAlbum = null;
            savedPictures = null;
            songs = null;
            artists = null;
            genres = null;
            playlists = null;
            albums = null;

            if (nativePlayer != null)
            {
                nativePlayer.close();
                nativePlayer = null;
            }
        }
        #endregion
    }
}
