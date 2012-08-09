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
			get
			{
				throw new NotImplementedException();
			}
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

		~Album()
		{
			Dispose();
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
			throw new NotImplementedException();
		}

		public bool Equals(Album other)
		{
			throw new NotImplementedException();
		}

		public override bool Equals(object other)
		{
			throw new NotImplementedException();
		}

		public static bool operator ==(Album first, Album second)
		{
			throw new NotImplementedException();
		}

		public static bool operator !=(Album first, Album second)
		{
			throw new NotImplementedException();
		}

		public override string ToString()
		{
			throw new NotImplementedException();
		}

		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}
	}
}
