#region Using Statements
using System;
using ANX.Framework.NonXNA.SoundSystem;
using ANX.Framework;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.SoundSystem.Null
{
    public class NullAudioListener : IAudioListener
    {
        public NullAudioListener()
        {
            Forward = Vector3.Forward;
            Up = Vector3.Up;
        }

        public Vector3 Forward
        {
            get;
            set;
        }

        public Vector3 Position
        {
            get;
            set;
        }

        public Vector3 Up
        {
            get;
            set;
        }

        public Vector3 Velocity
        {
            get;
            set;
        }
    }
}
