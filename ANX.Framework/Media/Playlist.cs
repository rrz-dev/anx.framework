using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Untested)]
	public sealed class Playlist : IEquatable<Playlist>, IDisposable
    {
        internal string Id { get; private set; }

	    public bool IsDisposed { get; private set; }
        public string Name { get; internal set; }
        public SongCollection Songs { get; internal set; }
        public TimeSpan Duration { get; internal set; }

		internal Playlist(string setId)
		{
		    Id = setId;
			IsDisposed = false;
		}

        public bool Equals(Playlist other)
		{
		    return Id == other.Id;
		}

		public override bool Equals(object obj)
        {
            return obj is Playlist ? Equals(obj as Playlist) : base.Equals(obj);
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
		
		public static bool operator ==(Playlist first, Playlist second)
		{
			return first.Equals(second);
		}

		public static bool operator !=(Playlist first, Playlist second)
		{
			return first.Equals(second) == false;
		}
	}
}
