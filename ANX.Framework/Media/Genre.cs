using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.InProgress)]
    public sealed class Genre : IEquatable<Genre>, IDisposable
    {
        public bool IsDisposed { get; private set; }
        public string Name { get; private set; }
        public SongCollection Songs { get; internal set; }
        public AlbumCollection Albums { get; internal set; }

        internal Genre(string setName)
        {
            Name = setName;
            IsDisposed = false;
        }

        public bool Equals(Genre other)
        {
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            return obj is Genre ? Equals(obj as Genre) : base.Equals(obj);
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

        public static bool operator ==(Genre first, Genre second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Genre first, Genre second)
        {
            return first.Equals(second) == false;
        }
    }
}
