using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
	public sealed class PictureAlbum : IEquatable<PictureAlbum>, IDisposable
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

		public PictureAlbumCollection Albums
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public PictureCollection Pictures
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public PictureAlbum Parent
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		~PictureAlbum()
		{
			Dispose();
		}

		public bool Equals(PictureAlbum other)
		{
			throw new NotImplementedException();
		}

		public override bool Equals(object obj)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
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

		public static bool operator ==(PictureAlbum first, PictureAlbum second)
		{
			throw new NotImplementedException();
		}

		public static bool operator !=(PictureAlbum first, PictureAlbum second)
		{
			throw new NotImplementedException();
		}
	}
}
