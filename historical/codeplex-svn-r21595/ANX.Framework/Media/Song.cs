using System;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.SoundSystem;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.InProgress)]
    public sealed class Song : IEquatable<Song>, IDisposable
    {
        internal string Id { get; private set; }
        internal ISong NativeSong { get; private set; }
        internal MediaState State
        {
            get { return NativeSong.State; }
        }

        #region Public
        public bool IsDisposed { get; private set; }
        public string Name { get; private set; }
        public int Rating { get; internal set; }
        public int TrackNumber { get; internal set; }
        public int PlayCount { get; internal set; }
        public bool IsProtected { get; internal set; }
        public Artist Artist { get; internal set; }
        public Album Album { get; internal set; }
        public Genre Genre { get; internal set; }

        public bool IsRated
        {
            get { return Rating > 0; }
        }

        public TimeSpan Duration
        {
            get { return NativeSong.Duration; }
        }
        #endregion

        #region Constructor
        internal Song(string setName, string setId, string filename, int duration)
        {
            var creator = AddInSystemFactory.Instance.GetDefaultCreator<ISoundSystemCreator>();
            NativeSong = creator.CreateSong(this, filename, duration);
            Id = setId;
            Name = setName;
            IsDisposed = false;
        }

        internal Song(string setName, Uri uri)
        {
            var creator = AddInSystemFactory.Instance.GetDefaultCreator<ISoundSystemCreator>();
            NativeSong = creator.CreateSong(this, uri);
            Id = "-1";
            Name = setName;
            IsDisposed = false;
        }

        internal Song(string setName, string filename, int duration)
        {
            var creator = AddInSystemFactory.Instance.GetDefaultCreator<ISoundSystemCreator>();
            NativeSong = creator.CreateSong(this, filename, duration);
            Id = "-1";
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

        #region Equals
        public bool Equals(Song other)
        {
            if (other == null || Id != other.Id)
                return false;

            return Id != "-1" || ReferenceEquals(this, other);
        }

        public override bool Equals(object obj)
        {
            if (obj is Song && ReferenceEquals(this, obj) == false)
                return Equals((Song)obj);

            return base.Equals(obj);
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            if (NativeSong != null)
                NativeSong.Dispose();
            NativeSong = null;
        }
        #endregion

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
            return object.Equals(first, second);
        }

        public static bool operator !=(Song first, Song second)
        {
            return !(first == second);
        }
        #endregion
    }
}
