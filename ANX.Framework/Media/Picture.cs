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

		#region Constructor
		private Picture()
		{
			IsDisposed = false;
		}

		~Picture()
		{
			Dispose();
		}
		#endregion

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
			if (obj is Picture)
				return Equals(obj as Picture);

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
		public static bool operator ==(Picture first, Picture second)
		{
			return first.Equals(second);
		}

		public static bool operator !=(Picture first, Picture second)
		{
			return first.Equals(second) == false;
		}
		#endregion
	}
}
