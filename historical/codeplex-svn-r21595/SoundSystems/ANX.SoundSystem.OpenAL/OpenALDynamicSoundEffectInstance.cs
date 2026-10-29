using System;
using ANX.Framework;
using ANX.Framework.Audio;
using ANX.Framework.NonXNA.Development;
using ANX.Framework.NonXNA.SoundSystem;
using OpenTK.Audio.OpenAL;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.SoundSystem.OpenAL
{
    [Developer("AstrorEnales")]
    public class OpenALDynamicSoundEffectInstance : IDynamicSoundEffectInstance
    {
        private int source;
        private readonly int sampleRate;
        private readonly int channels;
        private readonly ALFormat format;

        public event EventHandler<EventArgs> BufferNeeded;

        public int PendingBufferCount
        {
            get
            {
                int queueSize;
                AL.GetSource(source, ALGetSourcei.BuffersQueued, out queueSize);
                return queueSize;
            }
        }

        public bool IsLooped
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public float Pan
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public float Pitch
        {
            get
            {
                float value;
                AL.GetSource(source, ALSourcef.Pitch, out value);
                return value;
            }
            set { AL.Source(source, ALSourcef.Pitch, value); }
        }

        public SoundState State { get; private set; }

        public float Volume
        {
            get
            {
                float value;
                AL.GetSource(source, ALSourcef.Gain, out value);
                return value;
            }
            set { AL.Source(source, ALSourcef.Gain, value); }
        }

        public OpenALDynamicSoundEffectInstance(int setSampleRate, AudioChannels setChannels)
        {
            State = SoundState.Stopped;
            sampleRate = setSampleRate;
            channels = (int)setChannels;
            format = channels == 1 ? ALFormat.Mono16 : ALFormat.Stereo16;
            source = AL.GenSource();

            FrameworkDispatcher.OnUpdate += Tick;
        }

        public void SubmitBuffer(byte[] buffer)
        {
            SubmitBuffer(buffer, 0, buffer.Length);
        }

        public unsafe void SubmitBuffer(byte[] buffer, int offset, int count)
        {
            int bufferHandle = AL.GenBuffer();
            IntPtr dataHandle;
            fixed (byte* ptr = &buffer[0])
                dataHandle = (IntPtr)(ptr + offset);
            AL.BufferData(bufferHandle, format, dataHandle, count, sampleRate);
            AL.SourceQueueBuffer(source, bufferHandle);
        }

        public void Play()
        {
            if (State != SoundState.Playing)
                AL.SourcePlay(source);
        }

        public void Pause()
        {
            if (State != SoundState.Paused)
                AL.SourcePause(source);
        }

        public void Stop(bool immediate)
        {
            // TODO: handle immediate!

            if (State != SoundState.Stopped)
                AL.SourceStop(source);
        }

        public void Resume()
        {
            Play();
        }

        private void Tick()
        {
            if (State != SoundState.Playing)
                return;

            int buffersProcessed;
            AL.GetSource(source, ALGetSourcei.BuffersProcessed, out buffersProcessed);
            for (int index = 0; index < buffersProcessed; index++)
            {
                int buffer = AL.SourceUnqueueBuffer(source);
                AL.DeleteBuffer(buffer);
            }

            if (PendingBufferCount == 0 && BufferNeeded != null)
                BufferNeeded(null, EventArgs.Empty);
        }

        public void Apply3D(AudioListener[] listeners, AudioEmitter emitter)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (source != -1)
            {
                FrameworkDispatcher.OnUpdate -= Tick;
                AL.DeleteSource(source);
            }

            source = -1;
        }
    }
}
