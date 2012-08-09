using System;
using System.IO;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
	public sealed class Picture : IEquatable<Picture>, IDisposable
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

		public PictureAlbum Album
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int Width
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int Height
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public DateTime Date
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		~Picture()
		{
			Dispose();
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

		public static bool operator ==(Picture first, Picture second)
		{
			throw new NotImplementedException();
		}

		public static bool operator !=(Picture first, Picture second)
		{
			throw new NotImplementedException();
		}
	}
}
