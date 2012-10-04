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
        private FileStream oggFileStream;
        private XAudioOggInputStream oggStream;
        private SourceVoice source;
        private readonly AudioBuffer[] buffers = new AudioBuffer[2];
        private int nextBufferIndex;

        public TimeSpan Duration { get; private set; }
        public TimeSpan PlayPosition { get; private set; }
        public MediaState State { get; private set; }

        public XAudioSong(XAudio2 device, Uri uri)
        {
            string path = uri.AbsolutePath.Replace("%20", "");
            Init(device, path);
            // TODO: duration
        }

        public XAudioSong(XAudio2 device, string filepath, int duration)
        {
            Init(device, filepath);
            Duration = new TimeSpan(0, 0, 0, 0, duration);
        }

        private void Init(XAudio2 device, string filepath)
        {
            PlayPosition = TimeSpan.Zero;
            State = MediaState.Stopped;

            oggFileStream = File.Open(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            oggStream = new XAudioOggInputStream(oggFileStream);
            var format = new WaveFormat(oggStream.SampleRate, 16, oggStream.Channels);
            source = new SourceVoice(device, format, true);
            source.BufferEnd += StreamBuffer;

            for (int index = 0; index < buffers.Length; index++)
                buffers[index] = new AudioBuffer { Stream = new DataStream(XAudioOggInputStream.BufferLength, false, true) };
        }

        private void StreamBuffer(IntPtr handle)
        {
            if (Stream() == false)
                Stop();
        }

        public void Play()
        {
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
            oggFileStream.Position = 0;
            oggStream = new XAudioOggInputStream(oggFileStream);
        }

        internal bool Stream()
        {
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
        }

        public void Dispose()
        {
            if (oggFileStream != null)
                oggFileStream.Close();

            oggFileStream = null;
            oggStream = null;
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
