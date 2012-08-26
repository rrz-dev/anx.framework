using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
	public sealed class Song : IEquatable<Song>, IDisposable
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

		public bool IsRated
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public Artist Artist
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public Album Album
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public Genre Genre
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

		public int Rating
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int PlayCount
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int TrackNumber
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool IsProtected
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		#region Constructor
		private Song()
		{
			IsDisposed = false;
		}

		~Song()
		{
			Dispose();
		}
		#endregion

		public Song FromUri(string name, Uri uri)
		{
			throw new NotImplementedException();
		}

		public bool Equals(Song other)
		{
			throw new NotImplementedException();
		}

		public override bool Equals(object obj)
		{
			if (obj is Song)
				return Equals(obj as Song);

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

		#region ToString
		public override string ToString()
		{
			return Name;
		}
		#endregion

		#region GetHashCode
		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}
		#endregion

		#region Operator overloading
		public static bool operator ==(Song first, Song second)
		{
			return first.Equals(second);
		}

		public static bool operator !=(Song first, Song second)
		{
			return first.Equals(second) == false;
		}
		#endregion
	}
}
