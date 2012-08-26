using System;
using System.IO;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
	public sealed class Album : IEquatable<Album>, IDisposable
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

		public TimeSpan Duration
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool HasArt
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

		public SongCollection Songs
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

		#region Constructor
		private Album()
		{
			IsDisposed = false;
		}

		~Album()
		{
			Dispose();
		}
		#endregion

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
			if (IsDisposed == false)
			{
				IsDisposed = true;
				throw new NotImplementedException();
			}
		}

		public bool Equals(Album other)
		{
			throw new NotImplementedException();
		}

		public override bool Equals(object obj)
		{
			if (obj is Album)
				return Equals(obj as Album);

			return base.Equals(obj);
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
		public static bool operator ==(Album first, Album second)
		{
			return first.Equals(second);
		}

		public static bool operator !=(Album first, Album second)
		{
			return first.Equals(second) == false;
		}
		#endregion
	}
}
