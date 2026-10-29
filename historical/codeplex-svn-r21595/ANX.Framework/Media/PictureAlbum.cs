using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
    [PercentageComplete(50)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public sealed class PictureAlbum : IEquatable<PictureAlbum>, IDisposable
    {
        internal string Id { get; private set; }

        public bool IsDisposed { get; private set; }
        public string Name { get; private set; }
        public PictureAlbumCollection Albums { get; private set; }
        public PictureCollection Pictures { get; private set; }
        public PictureAlbum Parent { get; private set; }

        internal PictureAlbum(string setId)
        {
            Id = setId;
            IsDisposed = false;
        }

        public bool Equals(PictureAlbum other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return obj is PictureAlbum ? Equals(obj as PictureAlbum) : base.Equals(obj);
        }

        public void Dispose()
        {
            IsDisposed = true;
        }

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public static bool operator ==(PictureAlbum first, PictureAlbum second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(PictureAlbum first, PictureAlbum second)
        {
            return first.Equals(second) == false;
        }
    }
}
