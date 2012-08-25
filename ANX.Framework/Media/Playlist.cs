using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
	public sealed class Playlist : IEquatable<Playlist>, IDisposable
	{
		public bool IsDisposed
		{
			get;
			private set;
		}

		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public SongCollection Songs
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public TimeSpan Duration
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		internal Playlist()
		{
		}

		~Playlist()
		{
			Dispose();
		}

		public bool Equals(Playlist other)
		{
			throw new NotImplementedException();
		}

		public override bool Equals(object obj)
		{
			if (obj is Playlist)
				return Equals(obj as Playlist);

			return base.Equals(obj);
		}

		public void Dispose()
		{
			if (IsDisposed == false)
			{
				IsDisposed = true;
				throw new NotImplementedException();
			}
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
