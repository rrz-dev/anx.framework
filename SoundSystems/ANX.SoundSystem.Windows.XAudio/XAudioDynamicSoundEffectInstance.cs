using System;
using ANX.Framework.Audio;
using ANX.Framework.NonXNA.Development;
using ANX.Framework.NonXNA.SoundSystem;
using SharpDX;
using SharpDX.Multimedia;
using SharpDX.XAudio2;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.SoundSystem.Windows.XAudio
{
    /// <summary>
    /// DynamicSoundEffectInstance supports only PCM 16-bit mono or stereo data.
    /// </summary>
    [Developer("AstrorEnales")]
    public class XAudioDynamicSoundEffectInstance : IDynamicSoundEffectInstance
    {
        private SourceVoice source;
        private float currentPitch;

        public event EventHandler<EventArgs> BufferNeeded;

        public int PendingBufferCount
        {
            get { return source.State.BuffersQueued; }
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
            get { return currentPitch; }
            set
            {
                currentPitch = value;
                source.SetFrequencyRatio(value);
                // TODO: pitch <= 1 is working, but greater isn't
                if (value > 1f)
                    throw new NotImplementedException("Pitch greater than 1f is currently not implemented for XAudio!");
            }
        }

        public SoundState State { get; private set; }

        public float Volume
        {
            get { return source.Volume; }
            set { source.SetVolume(value, 0); }
        }

        public XAudioDynamicSoundEffectInstance(XAudio2 device, int sampleRate, AudioChannels channels)
        {
            State = SoundState.Stopped;
            currentPitch = 1f;
            var format = new WaveFormat(sampleRate, 16, (int)channels);
            source = new SourceVoice(device, format, true);
            source.BufferEnd += OnBufferEnd;
        }

        private void OnBufferEnd(IntPtr handle)
        {
            if (source.State.BuffersQueued == 0 && BufferNeeded != null)
                BufferNeeded(null, EventArgs.Empty);
        }

        public void SubmitBuffer(byte[] buffer)
        {
            SubmitBuffer(buffer, 0, buffer.Length);
        }

        public unsafe void SubmitBuffer(byte[] buffer, int offset, int count)
        {
            var audioBuffer = new AudioBuffer();
            IntPtr dataHandle;
            fixed (byte* ptr = &buffer[0])
                dataHandle = (IntPtr)(ptr + offset);
            audioBuffer.Stream = new DataStream(dataHandle, count, false, false);
            source.SubmitSourceBuffer(audioBuffer, null);
        }

        public void Play()
        {
            if (State != SoundState.Playing)
                source.Start();
        }

        public void Pause()
        {
            if (State != SoundState.Paused)
                source.Stop();
        }

        public void Stop(bool immediate)
        {
            // TODO: handle immediate

            if (State != SoundState.Stopped)
                source.Stop();
        }

        public void Resume()
        {
            Play();
        }

        public void Apply3D(AudioListener[] listeners, AudioEmitter emitter)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (source != null)
            {
                source.BufferEnd -= OnBufferEnd;
                source.DestroyVoice();
                source.Dispose();
            }

            source = null;
        }
    }
}
