using System;
using System.IO;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
    [PercentageComplete(70)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public sealed class Album : IEquatable<Album>, IDisposable
    {
        internal string Id { get; private set; }

        public bool IsDisposed { get; private set; }
        public string Name { get; internal set; }
        public TimeSpan Duration { get; internal set; }
        public bool HasArt { get; internal set; }
        public Artist Artist { get; internal set; }
        public SongCollection Songs { get; internal set; }
        public Genre Genre { get; internal set; }

        internal Album(string setId)
        {
            Id = setId;
            IsDisposed = false;
        }

        public Stream GetAlbumArt()
        {
            throw new NotImplementedException();
        }

        public Stream GetThumbnail()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            IsDisposed = true;
        }

        public bool Equals(Album other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return obj is Album ? Equals(obj as Album) : base.Equals(obj);
        }

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public static bool operator ==(Album first, Album second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Album first, Album second)
        {
            return first.Equals(second) == false;
        }
    }
}
