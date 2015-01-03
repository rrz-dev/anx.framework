using System;
using System.IO;
using ANX.Framework.Audio;
using ANX.Framework.Media;
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
    [Developer("AstrorEnales")]
    public class XAudioSong : ISong
    {
#if !WINDOWSMETRO
        private FileStream oggFileStream;
        private XAudioOggInputStream oggStream;
#endif
        private SourceVoice source;
        private readonly AudioBuffer[] buffers = new AudioBuffer[2];
        private int nextBufferIndex;
        private XAudio2 device;
        private string filepath;
        private bool isInitialized;

        public TimeSpan Duration { get; private set; }
        public TimeSpan PlayPosition { get; private set; }
        public MediaState State { get; private set; }

        public XAudioSong(XAudio2 device, Uri uri)
        {
            filepath = Uri.UnescapeDataString(uri.AbsolutePath);
            this.device = device;
            // TODO: duration
        }

        public XAudioSong(XAudio2 device, string filepath, int duration)
        {
            this.filepath = filepath;
            this.device = device;
            Duration = new TimeSpan(0, 0, 0, 0, duration);
        }

        private void Init()
        {
            if (Path.GetExtension(filepath).ToLower() != ".ogg")
                throw new NotImplementedException("Currently only ogg playback is implemented!");

            isInitialized = true;
            PlayPosition = TimeSpan.Zero;
            State = MediaState.Stopped;

            //TODO: Provide a Metro implementation.
#if !WINDOWSMETRO
            oggFileStream = File.Open(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            oggStream = new XAudioOggInputStream(oggFileStream);
            var format = new WaveFormat(oggStream.SampleRate, 16, oggStream.Channels);
            source = new SourceVoice(device, format, true);
            source.BufferEnd += StreamBuffer;

            for (int index = 0; index < buffers.Length; index++)
                buffers[index] = new AudioBuffer { Stream = new DataStream(XAudioOggInputStream.BufferLength, false, true) };
#endif
        }

        private void StreamBuffer(IntPtr handle)
        {
            if (Stream() == false)
                Stop();
        }

        public void Play()
        {
            if (isInitialized == false)
                Init();

            if (State == MediaState.Playing)
                return;

            if (State == MediaState.Stopped)
            {
                Rewind();

                for (int index = 0; index < buffers.Length; index++)
                    if (Stream() == false)
                        return;
            }

            source.Start();
            State = MediaState.Playing;
        }

        public void Stop()
        {
            if (State == MediaState.Stopped)
                return;

            State = MediaState.Stopped;
            source.Stop();
            source.FlushSourceBuffers();
        }

        public void Pause()
        {
            if (State == MediaState.Paused)
                return;

            State = MediaState.Paused;
            source.Stop();
        }

        public void Resume()
        {
            Play();
        }

        public void Update()
        {
        }

        public void GetVisualizationData(VisualizationData data)
        {
            throw new NotImplementedException();
        }

        internal void Rewind()
        {
            PlayPosition = TimeSpan.Zero;
#if !WINDOWSMETRO
            oggFileStream.Position = 0;
            oggStream = new XAudioOggInputStream(oggFileStream);
#endif
        }

        internal bool Stream()
        {
#if !WINDOWSMETRO
            AudioBuffer currentBuffer = buffers[nextBufferIndex];
            currentBuffer.Stream.Position = 0;
            int size = oggStream.Read(currentBuffer.Stream);
            if (size <= 0)
                return false;

            var channels = (AudioChannels)oggStream.Channels;
            PlayPosition = PlayPosition.Add(SoundEffect.GetSampleDuration(size, oggStream.SampleRate, channels));
            currentBuffer.PlayLength = size / 4;
            source.SubmitSourceBuffer(currentBuffer, null);
            nextBufferIndex = (nextBufferIndex + 1) % buffers.Length;

            return true;
#else
            return false;
#endif
        }

        public void Dispose()
        {
#if !WINDOWSMETRO
            if (oggFileStream != null)
            {
                oggFileStream.Close();
                oggFileStream.Dispose();
                oggFileStream = null;
            }

            if (oggStream != null)
            {
                oggStream = null;
            }
#endif

            if (source != null)
            {
                source.FlushSourceBuffers();
                source.DestroyVoice();
                source.Dispose();
            }

            source = null;
        }
    }
}
