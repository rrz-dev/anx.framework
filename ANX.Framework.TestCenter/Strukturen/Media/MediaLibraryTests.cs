using System;
using NUnit.Framework;
using ANXMediaLibrary = ANX.Framework.Media.MediaLibrary;
using XNAMediaLibrary = Microsoft.Xna.Framework.Media.MediaLibrary;

namespace ANX.Framework.TestCenter.Strukturen.Media
{
    public static class MediaLibraryTests
    {
        [Test]
        public static void TestGetSongs()
        {
            var anxLibrary = new ANXMediaLibrary();
            var xnaLibrary = new XNAMediaLibrary();
            var anxCollection = anxLibrary.Songs;
            var xnaCollection = xnaLibrary.Songs;
            Assert.AreEqual(xnaCollection.Count, anxCollection.Count);
            for(int index = 0; index < xnaCollection.Count; index++)
            {
                Assert.AreEqual(xnaCollection[index].Name, anxCollection[index].Name);
                Assert.AreEqual(xnaCollection[index].Rating, anxCollection[index].Rating);
                Assert.AreEqual(xnaCollection[index].IsRated, anxCollection[index].IsRated);
                Assert.AreEqual(xnaCollection[index].TrackNumber, anxCollection[index].TrackNumber);
                Assert.AreEqual(xnaCollection[index].PlayCount, anxCollection[index].PlayCount);
                Assert.AreEqual(xnaCollection[index].IsProtected, anxCollection[index].IsProtected);
                Assert.True(AreNearlyEqual(xnaCollection[index].Duration, anxCollection[index].Duration));

                Assert.AreEqual(xnaCollection[index].Artist.Name, anxCollection[index].Artist.Name);
                Assert.AreEqual(xnaCollection[index].Album.Name, anxCollection[index].Album.Name);
                Assert.AreEqual(xnaCollection[index].Album.Artist.Name, anxCollection[index].Album.Artist.Name);
                Assert.AreEqual(xnaCollection[index].Genre.Name, anxCollection[index].Genre.Name);
            }
        }

        private static bool AreNearlyEqual(TimeSpan first, TimeSpan second)
        {
            long ticks1 = first.Ticks;
            long ticks2 = second.Ticks;
            return ticks1 - 10000 <= ticks2 && ticks1 + 10000 >= ticks2;
        }

        [Test]
        public static void TestGetAlbums()
        {
            var anxLibrary = new ANXMediaLibrary();
            var xnaLibrary = new XNAMediaLibrary();
            var anxCollection = anxLibrary.Albums;
            var xnaCollection = xnaLibrary.Albums;
            Assert.AreEqual(xnaCollection.Count, anxCollection.Count);
            for (int index = 0; index < xnaCollection.Count; index++)
            {
                Assert.AreEqual(xnaCollection[index].Name, anxCollection[index].Name);
            }
        }

        [Test]
        public static void TestGetPictures()
        {
            var anxLibrary = new ANXMediaLibrary();
            var xnaLibrary = new XNAMediaLibrary();
            var anxCollection = anxLibrary.Pictures;
            var xnaCollection = xnaLibrary.Pictures;
            Assert.AreEqual(xnaCollection.Count, anxCollection.Count);
            for (int index = 0; index < xnaCollection.Count; index++)
            {
                Assert.AreEqual(xnaCollection[index].Name, anxCollection[index].Name);
            }
        }

        [Test]
        public static void TestGetArtists()
        {
            var anxLibrary = new ANXMediaLibrary();
            var xnaLibrary = new XNAMediaLibrary();
            var anxCollection = anxLibrary.Artists;
            var xnaCollection = xnaLibrary.Artists;
            Assert.AreEqual(xnaCollection.Count, anxCollection.Count);
            for (int index = 0; index < xnaCollection.Count; index++)
            {
                Assert.AreEqual(xnaCollection[index].Name, anxCollection[index].Name);
            }
        }

        [Test]
        public static void TestGetGenres()
        {
            var anxLibrary = new ANXMediaLibrary();
            var xnaLibrary = new XNAMediaLibrary();
            var anxCollection = anxLibrary.Genres;
            var xnaCollection = xnaLibrary.Genres;
            Assert.AreEqual(xnaCollection.Count, anxCollection.Count);
            for (int index = 0; index < xnaCollection.Count; index++)
            {
                Assert.AreEqual(xnaCollection[index].Name, anxCollection[index].Name);
            }
        }
    }
}
