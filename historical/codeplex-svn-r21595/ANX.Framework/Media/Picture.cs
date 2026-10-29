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
    public sealed class Picture : IEquatable<Picture>, IDisposable
    {
        internal string Id { get; private set; }

        public bool IsDisposed { get; private set; }
        public string Name { get; internal set; }
        public PictureAlbum Album { get; internal set; }
        public int Width { get; internal set; }
        public int Height { get; internal set; }
        public DateTime Date { get; internal set; }

        internal Picture(string setId)
        {
            Id = setId;
            IsDisposed = false;
        }

        public Stream GetImage()
        {
            throw new NotImplementedException();
        }

        public Stream GetThumbnail()
        {
            throw new NotImplementedException();
        }

        public bool Equals(Picture other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return obj is Picture ? Equals(obj as Picture) : base.Equals(obj);
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

        public static bool operator ==(Picture first, Picture second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Picture first, Picture second)
        {
            return first.Equals(second) == false;
        }
    }
}
