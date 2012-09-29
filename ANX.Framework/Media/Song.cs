using System;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.SoundSystem;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
    [PercentageComplete(50)]
    [Developer("AstrorEnales")]
	public sealed class Song : IEquatable<Song>, IDisposable
	{
        internal ISong NativeSong { get; private set; }
        internal MediaState State { get { return NativeSong.State; } }

        #region Public
        public bool IsDisposed { get; private set; }
	    public string Name { get; private set; }

	    public bool IsRated
	    {
	        get { return Rating > 0; }
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
			get { return NativeSong.Duration; }
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
        #endregion

		#region Constructor
		internal Song(string setName, Uri uri)
		{
            NativeSong = AddInSystemFactory.Instance.GetDefaultCreator<ISoundSystemCreator>().CreateSong(this, uri);
			Name = setName;
			IsDisposed = false;
		}

		~Song()
		{
			Dispose();
		}
		#endregion

		public static Song FromUri(string name, Uri uri)
		{
		    return new Song(name, uri);
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
		    if (IsDisposed)
		        return;

		    IsDisposed = true;

            if(NativeSong != null)
                NativeSong.Dispose();
		    NativeSong = null;
		}

        internal void Play()
        {
            NativeSong.Play();
        }

        internal void Stop()
        {
            NativeSong.Stop();
        }

        internal void Pause()
        {
            NativeSong.Pause();
        }

        internal void Resume()
        {
            NativeSong.Resume();
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
			return first != null && first.Equals(second);
		}

		public static bool operator !=(Song first, Song second)
		{
			return first == null || first.Equals(second) == false;
		}
		#endregion
	}
}
