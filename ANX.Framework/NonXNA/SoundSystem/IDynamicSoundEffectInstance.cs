using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.SoundSystem
{
    public interface IDynamicSoundEffectInstance : ISoundEffectInstance
    {
        event EventHandler<EventArgs> BufferNeeded;
        int PendingBufferCount { get; }

        void SubmitBuffer(byte[] buffer);
        void SubmitBuffer(byte[] buffer, int offset, int count);
    }
}