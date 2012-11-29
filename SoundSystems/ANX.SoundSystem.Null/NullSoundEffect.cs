#region Using Statements
using System;
using ANX.Framework.NonXNA.SoundSystem;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.SoundSystem.Null
{
    public class NullSoundEffect : ISoundEffect
    {
        public TimeSpan Duration
        {
            get { return TimeSpan.Zero; }
        }

        public void Dispose()
        {
        }
    }
}
