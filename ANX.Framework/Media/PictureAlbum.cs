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

		#region Constructor
		private PictureAlbum()
		{
			IsDisposed = false;
		}

		~PictureAlbum()
		{
			Dispose();
		}
		#endregion

		public bool Equals(PictureAlbum other)
		{
			throw new NotImplementedException();
		}

		public override bool Equals(object obj)
		{
			if (obj is PictureAlbum)
				return Equals(obj as PictureAlbum);

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
		public static bool operator ==(PictureAlbum first, PictureAlbum second)
		{
			return first.Equals(second);
		}

		public static bool operator !=(PictureAlbum first, PictureAlbum second)
		{
			return first.Equals(second) == false;
		}
		#endregion
	}
}
