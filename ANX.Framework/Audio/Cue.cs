using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Audio
{
    [PercentageComplete(5)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public sealed class Cue : IDisposable
    {
        #region Events
        public event EventHandler<EventArgs> Disposing;
        #endregion

        #region Public
        public bool IsCreated
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsDisposed
        {
            get;
            private set;
        }

        public bool IsPaused
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsPlaying
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsPrepared
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsPreparing
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsStopped
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsStopping
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
        #endregion

        #region Constructor
        internal Cue()
        {
        }

        ~Cue()
        {
            Dispose();
        }
        #endregion

        #region Apply3D
        public void Apply3D(AudioListener listener, AudioEmitter emitter)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region SetVariable
        public void SetVariable(string name, float value)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region GetVariable
        public float GetVariable(string name)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Pause
        public void Pause()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Play
        public void Play()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Resume
        public void Resume()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Stop
        public void Stop(AudioStopOptions options)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            if (IsDisposed == false)
            {
                IsDisposed = true;
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}
